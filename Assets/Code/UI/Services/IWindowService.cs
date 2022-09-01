using Code.Services;

namespace Code.UI.Services
{
    public interface IWindowService : IService
    {
        void Open(UIWindowID windowId);
    }
}