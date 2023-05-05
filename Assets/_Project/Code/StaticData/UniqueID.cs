using DG.Tweening.Plugins.Core.PathCore;
using UnityEditor;
using UnityEngine;

namespace Code.StaticData
{

    [CreateAssetMenu(fileName = "UniqueID", menuName = "Custom/UniqueID")]
    public class UniqueID : ScriptableObject
    {
        public string Name;
        public int ID;
    }
}
