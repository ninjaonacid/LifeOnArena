using Code.UI.Model;
using Code.UI.View;

namespace Code.UI.Controller
{
    public interface IScreenController<TModel, TView> 
        where TModel : IScreenModel 
        where TView : BaseView
    {
        void InitController(TModel model, TView view);
    }
}
