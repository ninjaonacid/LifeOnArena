using Code.Services;

namespace Code.UI.Services
{
    public interface IScreenService : IService
    {
        void Open(ScreenID screenId);
        void Close(ScreenID screenID);
        void Cleanup();
    }
}