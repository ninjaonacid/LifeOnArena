using Code.Runtime.Services;

namespace Code.Runtime.UI.Services
{
    public interface IScreenService : IService
    {
        void Open(ScreenID screenId);
        void Close(ScreenID screenID);
        void Cleanup();
    }
}