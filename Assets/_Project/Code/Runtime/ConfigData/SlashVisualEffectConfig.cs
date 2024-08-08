using Sirenix.OdinInspector;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Code.Runtime.ConfigData
{
    public enum SlashDirection
    {
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3,
        LeftUp = 4,
        RightUp = 5,
        LeftDown = 6,
        RightDown = 7,
    }
    
    [CreateAssetMenu(fileName = "SlashVisualEffect", menuName = "Config/VisualEffects/SlashVisualEffect")]
    public class SlashVisualEffectConfig : ScriptableObject
    {
        [Title("SlashEffect")]
        public VisualEffectData VisualEffect;
        [Title("SlashSize")]
        public Vector3 SlashSize;
    }
}