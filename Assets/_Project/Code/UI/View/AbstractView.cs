using System;
using Code.UI.Model;
using UnityEngine;

namespace Code.UI.View
{
    public class AbstractView<TModel> : BaseView where TModel : IScreenModel
    {

    }
}
