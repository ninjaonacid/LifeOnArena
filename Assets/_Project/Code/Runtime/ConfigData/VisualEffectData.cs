using Code.Runtime.ConfigData.Identifiers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code.Runtime.ConfigData
{
    [CreateAssetMenu(fileName = "VisualEffect", menuName = "Config/VisualEffects")]
    public class VisualEffectData : ScriptableObject
    {
        public VisualEffectIdentifier Identifier;
        public AssetReference PrefabReference;
    }
}
