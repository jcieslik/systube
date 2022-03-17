using Data.Enums;
using Services.Interfaces;

namespace Services.Services
{
    public class VideoService : IVideoService
    {
        public string GetVideoPathByIdAndResolution(int fileId, Resolution resolution)
        {
            return "C:/Users/jcies/Downloads/symulacja.mp4";
        }
    }
}
