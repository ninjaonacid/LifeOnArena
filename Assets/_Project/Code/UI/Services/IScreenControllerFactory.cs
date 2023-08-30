using Code.UI.Controller;
using Code.UI.Model;
using Code.UI.View;
using UnityEngine;

namespace Code.UI.Services
{
    public interface IScreenControllerFactory
    {
        TController CreateController<TModel, TView, TController>();
    }
}
