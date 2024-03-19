using Code.Runtime.ConfigData;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem
{
    public class AbilityView
    {
        public AbilityView(Sprite icon)
        {
            Icon = icon;
        }

        public AbilityView(Sprite icon, VisualEffectData visualEffectData)
        {
            Icon = icon;
            VisualEffectData = visualEffectData;
        }

        public Sprite Icon { get; }
        
        public VisualEffectData VisualEffectData { get; }

    }
}