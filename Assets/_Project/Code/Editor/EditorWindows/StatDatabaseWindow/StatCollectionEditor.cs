using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Modules.StatSystem.Runtime;
using UnityEngine.UIElements;

namespace Code.Editor.EditorWindows.StatDatabaseWindow
{
    public class StatCollectionEditor : ScriptableObjectCollectionEditor<StatDefinition>
    {
        public new class UxmlFactory : UxmlFactory<StatCollectionEditor, UxmlTraits>
        {
            
        }
    }
}
