using UnityEngine;

namespace Code.UI.Screen
{
    public abstract class BaseScreenView : MonoBehaviour
    {
        public abstract void Show();

        public abstract void Hide();

        public abstract void Close();
        
    }
}
