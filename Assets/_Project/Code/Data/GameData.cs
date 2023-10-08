using System;
using UnityEngine;

namespace Code.Data
{
    [Serializable]
    public class GameData 
    {
        public PlayerData PlayerData { get; set; }
        
        public AudioData AudioData { get; set; }
        
    }
}
