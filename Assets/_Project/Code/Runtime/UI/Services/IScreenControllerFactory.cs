using System;
using Code.Runtime.UI.Controller;

namespace Code.Runtime.UI.Services
{
    public interface IScreenControllerFactory
    {
        IScreenController CreateController(Type controller);
    }
}
