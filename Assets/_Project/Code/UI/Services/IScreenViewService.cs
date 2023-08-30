using Code.Services;
using Code.UI.Controller;
using Code.UI.Model;
using Code.UI.View;

namespace Code.UI.Services
{
    public interface IScreenViewService : IService
    {
        void Open(ScreenID windowId);

        void Show<TModel, TView, TController>(ScreenID screenID) 
            where TController : IScreenController<TModel, TView>
            where TModel : IScreenModel
            where TView : BaseView;
    }
}