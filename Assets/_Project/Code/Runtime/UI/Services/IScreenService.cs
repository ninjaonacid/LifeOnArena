using Code.Runtime.UI.Model;

namespace Code.Runtime.UI.Services
{
    public interface IScreenService
    {
        void Open(ScreenID screenId);
        void Close(ScreenID screenID);
        void Cleanup();
        void Open(ScreenID screenId, IScreenModelDto dto);
    }
}