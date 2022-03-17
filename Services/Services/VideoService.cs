using Data.Enums;
using Services.Interfaces;

namespace Services.Services
{
    public class VideoService : IVideoService
    {
        public string GetVideoPathByIdAndResolution(int fileId, Resolution resolution)
        {
            return "C:/Users/jcies/Downloads/sample_1280x720_surfing_with_audio.mp4";
        }
    }
}
