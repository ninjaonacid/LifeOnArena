using System;
using Code.UI.Controller;

namespace Code.UI.Services
{
    public interface IScreenControllerFactory
    {
        IScreenController CreateController(Type controller);
    }
}
