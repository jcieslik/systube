using Data.Enums;

namespace Services.Interfaces
{
    public interface IVideoService
    {
        string GetVideoPathByIdAndResolution(int fileId, Resolution resolution);
    }
}
