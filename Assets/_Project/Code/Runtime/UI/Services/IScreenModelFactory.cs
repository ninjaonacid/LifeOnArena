using System;
using Code.Runtime.UI.Model;

namespace Code.Runtime.UI.Services
{
    public interface IScreenModelFactory
    {
        IScreenModel CreateModel(Type model);
    }
}
