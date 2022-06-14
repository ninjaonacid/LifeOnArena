using UnityEngine;
using UnityEngine.UI;

namespace SimpleInputNamespace
{
    // Credit: http://answers.unity.com/answers/1157876/view.html
    [RequireComponent(typeof(CanvasRenderer))]
    public class NonDrawingGraphic : Graphic
    {
        public override void SetMaterialDirty()
        {
        }

        public override void SetVerticesDirty()
        {
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}