using Code.UI.Model;
using Code.UI.Services;
using Code.UI.View;

namespace Code.UI.Controller
{
    public interface IScreenController
    {
        void InitController(IScreenModel model, BaseView view, IScreenService screenService);
        
    }
}
