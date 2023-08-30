using System;
using Code.UI.Model;
using UnityEngine;

namespace Code.UI.Services
{
    public interface IScreenModelFactory
    {
        TModel CreateModel<TModel>() where TModel : IScreenModel;
        IScreenModel CreateModel(Type model);
    }
}
