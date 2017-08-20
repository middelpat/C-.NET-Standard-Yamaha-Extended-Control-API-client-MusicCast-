namespace YamahaMusicCast.Results
{
    public class MusicCastResult
    {
        public MusicCastResult(int statusCode)
        {
            Success = statusCode == 0;
            ResponseCode = statusCode;
            Message = Helpers.GetMessageForResponseCode(statusCode);
        }

        public bool Success { get; private set; }

        public int ResponseCode { get; private set; }

        public string Message { get; private set; }
    }
}
