using Code.Runtime.UI.Model;
using Code.Runtime.UI.Services;
using Code.Runtime.UI.View;

namespace Code.Runtime.UI.Controller
{
    public interface IScreenController
    {
        void InitController(IScreenModel model, BaseWindowView windowView, ScreenService screenService);
        
    }
}
