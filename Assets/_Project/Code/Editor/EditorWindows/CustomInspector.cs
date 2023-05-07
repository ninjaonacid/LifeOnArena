using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Code.Editor.EditorWindows
{
    public class CustomInspector : InspectorElement
    {
        public new class UxmlFactory : UxmlFactory<CustomInspector, UxmlTraits>
        {

        }
    }
}
