using System;
using System.Net;
using System.Net.Http;
using System.Linq;
using YamahaMusicCast.Enums;
using System.Threading.Tasks;
using YamahaMusicCast.Results;
using System.Json;

namespace YamahaMusicCast
{
    public class MusicCastClient : IMusicCastClient
    {
        private readonly IPAddress _deviceAddress;
        

        public MusicCastClient(IPAddress deviceAddress)
        {
            _deviceAddress = deviceAddress;
        }


        /// <summary>
        /// Turn the device on or off (standby)
        /// </summary>
        public async Task<MusicCastResult> ChangePower(PowerMode mode)
        {
            string power = mode.ToString().ToLowerInvariant();
            string command = $"{Constants.ZONE}/setPower?power={power}";

            return await _SendRequest(command);
        }


        /// <summary>
        /// Sets the volume using the decibel value as displayed on the display.
        /// Range: -80db (muted) and up. Default maxvalue = --20db as configurered in the MAX_VOLUME_STEP constant
        /// </summary>
        /// <param name="dbValue">the decibel value to set the volume to</param>
        public async Task<MusicCastResult> SetVolume(double dbValue)
        {
            // every volumestep = 0.5db
            // -80db = 1
            // -79.5db = 1.5
            // -50db = 61
            // -1 db = 160
            // etc.

            // this gives us the below formula for calculating the step (using the linear formula template y = mx + b)

            int step = (int)((2d * dbValue) + 161);

            return await SetVolumeStep(step);
        }


        /// <summary>
        /// Sets the volume in steps as used by the receiver. where 0 = -80db (minimum).
        /// Each step will increase the volume by 0.5 db. so 1 = -79.5db
        /// </summary>
        public async Task<MusicCastResult> SetVolumeStep(int step)
        {
            if (step < 0)
                step = 0;
            if (step > Constants.MAX_VOLUME_STEP)
                step = Constants.MAX_VOLUME_STEP;

            string command = $"{Constants.ZONE}/setVolume?volume={step}";

            return await _SendRequest(command);
        }


        /// <summary>
        /// Mute or unmute the device
        /// </summary>
        /// <param name="enable">true to mute, false to unmute</param>
        public async Task<MusicCastResult> SetMute(bool enable)
        {
            string value = enable.ToString().ToLowerInvariant();
            string command = $"{Constants.ZONE}/setMute?enable={value}";

            return await _SendRequest(command);
        }


        /// <summary>
        /// Change the input channel of the device
        /// </summary>
        public async Task<MusicCastResult> SetInput(Input input)
        {
            // todo: check /system/getFuncstatus to see if a call for prepareinputchange is needed (when prepare_input_change is present)
            var prepareResponse = await _prepareInputChange(input);

            if (!prepareResponse.Success)
                return prepareResponse;

            // todo implement /getfeatures and check if the specified feature is valid for this device
            string value = input.ToString().ToLowerInvariant();
            string command = $"{Constants.ZONE}/setInput?input={value}";

            return await _SendRequest(command);
        }

        
        private async Task<MusicCastResult> _prepareInputChange(Input input)
        {
            string value = input.ToString().ToLowerInvariant();
            string command = $"{Constants.ZONE}/prepareInputChange?input={value}";

            return await _SendRequest(command);
        }


        public async Task<MusicCastResult> SetHdmi(int portNum)
        {
            // todo select the correct enum and then call SetInput so prepareInputChange is reused

            var stringInputs = Helpers.GetEnumValues<Input>()
                .Select(i => i.ToString().ToLowerInvariant());

            string hdmiPort = "hdmi" + portNum;

            if (!stringInputs.Contains(hdmiPort))
                throw new ArgumentException($"{portNum} is no valid hdmi port. Take a look at the available hdmi ports in the 'Input' enum");

            string command = $"{Constants.ZONE}/setInput?input={hdmiPort}";

            return await _SendRequest(command);
        }


        // todo implement getavailableInputs (parse /system/getfeatures and return supported inputs)


        private async Task<MusicCastResult> _SendRequest(string cmd)
        {
            if (string.IsNullOrWhiteSpace(cmd))
                throw new ArgumentNullException(nameof(cmd));

            var client = new HttpClient();

            string requestUrl = $"http://{_deviceAddress.ToString()}/YamahaExtendedControl/v1/{cmd}";

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(requestUrl),
                Method = HttpMethod.Get,
            };

            request.Headers.Add("X-AppName", Constants.HEADER_APP_NAME);
            request.Headers.Add("X-AppPort", Constants.HEADER_APP_PORT);

            var result = await client.SendAsync(request);
            string responseStr = await result.Content.ReadAsStringAsync();

            var jsonData = JsonValue.Parse(responseStr);

            int responseCode;
            if(int.TryParse(jsonData["response_code"].ToString(), out responseCode))
            {
                return new MusicCastResult(responseCode);
            }

            throw new HttpRequestException("Unable to read a valid 'response_code' from the http response");
        }
    }
}
