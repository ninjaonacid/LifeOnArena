using System;
using Code.UI.Model;
using UnityEngine;

namespace Code.UI.Services
{
    public interface IScreenModelFactory
    {
        IScreenModel CreateModel<TModel>(ScreenID screenId);
        IScreenModel CreateModel(Type model);
    }
}
