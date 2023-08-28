using UnityEditor.Search;
using UnityEngine;

namespace Code.UI.View
{
    public interface IScreenView
    {
        void Show();

        void Hide();

        void Close();
    }
}
