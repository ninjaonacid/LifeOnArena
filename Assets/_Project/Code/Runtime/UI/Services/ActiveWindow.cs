using Code.Runtime.UI.Controller;
using Code.Runtime.UI.Model;
using Code.Runtime.UI.View;

namespace Code.Runtime.UI.Services
{
    public class ActiveWindow
    {
        public readonly IScreenController Controller;
        public readonly IScreenModel Model;
        public readonly BaseWindowView WindowView;
        public readonly ScreenID Id;

        public ActiveWindow(IScreenController controller, IScreenModel screenModel, BaseWindowView windowView, ScreenID id)
        {
            Controller = controller;
            Model = screenModel;
            WindowView = windowView;
            Id = id;
        }
    }
}