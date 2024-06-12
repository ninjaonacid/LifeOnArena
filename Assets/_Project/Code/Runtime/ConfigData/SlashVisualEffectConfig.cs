using Sirenix.OdinInspector;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
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
        [Title("SlashDirections")]
        [BoxGroup("SlashDirections")]
        public SlashDirection FirstSlash;
        [BoxGroup("SlashDirections")]
        public SlashDirection SecondSlash;
        [BoxGroup("SlashDirections")]
        public SlashDirection ThirdSlash;
        
        [Title("SlashDelays")]
        [BoxGroup("SlashDelays")]
        public float FirstSlashDelay;
        [BoxGroup("SlashDelays")]
        public float SecondSlashDelay;
        [BoxGroup("SlashDelays")]
        public float ThirdSlashDelay;
    }
}