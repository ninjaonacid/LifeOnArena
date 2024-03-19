using Code.Runtime.ConfigData;
using UnityEngine;

namespace Code.Runtime.Modules.AbilitySystem.ActiveAbilities
{
    public class AbilityView
    {
        public Sprite Icon;
        public VisualEffectData VisualEffectData;

        public AbilityView(Sprite icon, VisualEffectData visualEffectData = null)
        {
            Icon = icon;
            VisualEffectData = visualEffectData;
        }
    }
}