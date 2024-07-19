using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.ConfigData
{
    public enum PlayPosition
    {
        Up = 1,
        Down = 2,
        Center = 3
    }
    
    [CreateAssetMenu(fileName = "VisualEffect", menuName = "Config/VisualEffects/VisualEffect")]
    public class VisualEffectData : ScriptableObject
    {
        public VisualEffectIdentifier Identifier;
        public AssetReference PrefabReference;
        public PlayPosition PlayPosition = PlayPosition.Center;
    }
}
