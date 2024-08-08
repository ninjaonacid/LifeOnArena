using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.UI.AdaptiveGrid.Presets
{
    [Serializable]
    public class ScaleNone : AdaptivePreset
    {
        public override void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding)
        {
            //This strategy implements do nothing with content
            return;
        }

        public override System.Enum SelectorInInspector => Code.Runtime.UI.AdaptiveGrid.AdaptiveGrid.ScaleMethod.None;
    }
}