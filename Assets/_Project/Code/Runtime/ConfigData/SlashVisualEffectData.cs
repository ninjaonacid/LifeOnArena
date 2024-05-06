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
        Down = 3
    }
    
    [CreateAssetMenu(fileName = "SlashVisualEffect", menuName = "Config/VisualEffects/SlashVisualEffect")]
    public class SlashVisualEffectData : ScriptableObject
    {
        public VisualEffectData VisualEffect;
        public Quaternion FirstSlashRotation;
        public Vector3 SecondSlashRotation;
        
    }
}