using System;
using Code.UI.Model;
using UnityEngine;

namespace Code.UI.Services
{
    public interface IScreenModelFactory
    {
        IScreenModel CreateModel(Type model);
    }
}
