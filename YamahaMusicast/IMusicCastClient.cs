using System.Threading.Tasks;
using YamahaMusicCast.Enums;
using YamahaMusicCast.Results;

namespace YamahaMusicCast
{
    public interface IMusicCastClient
    {
        Task<MusicCastResult> ChangePower(PowerMode mode);

        Task<MusicCastResult> SetVolume(double dbValue);

        Task<MusicCastResult> SetVolumeStep(int step);

        Task<MusicCastResult> SetMute(bool enable);

        Task<MusicCastResult> SetInput(Input input);

        Task<MusicCastResult> SetHdmi(int portNum);
    }
}
