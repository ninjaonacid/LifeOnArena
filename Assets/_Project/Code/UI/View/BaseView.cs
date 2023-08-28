using System;
using UnityEditor.Search;
using UnityEngine;

namespace Code.UI.View
{
    public abstract class BaseView : MonoBehaviour, IDisposable
    {

        public abstract void Show();
        
        public abstract void Close();

        public abstract void Hide();

        public void Dispose()
        {
        }
    }
}
