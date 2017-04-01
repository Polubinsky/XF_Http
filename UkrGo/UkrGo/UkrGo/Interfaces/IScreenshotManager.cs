using System.Threading.Tasks;

namespace UkrGo.Interfaces
{
    public interface IScreenshotManager
    {
        Task<byte[]> CaptureAsync();
        void SavePictureToDisk(string filename, byte[] imageData);
    }
}
