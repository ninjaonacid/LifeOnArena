namespace Code.Runtime.UI.Services
{
    public interface IScreenService
    {
        void Open(ScreenID screenId);
        void Close(ScreenID screenID);
        void Cleanup();
    }
}