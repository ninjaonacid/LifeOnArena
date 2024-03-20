using Code.Runtime.UI.Controller;
using Code.Runtime.UI.View;

namespace Code.Runtime.UI.Services
{
    public class ActiveWindow
    {
        public readonly IScreenController Controller;
        public readonly BaseWindowView WindowView;
        public readonly ScreenID Id;

        public ActiveWindow(IScreenController controller, BaseWindowView windowView, ScreenID id)
        {
            Controller = controller;
            WindowView = windowView;
            Id = id;
        }
    }
}