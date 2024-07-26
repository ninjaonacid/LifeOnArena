using UnityEngine;

namespace Code.Runtime.ConfigData
{
    [CreateAssetMenu(menuName = "Config/AdvertisementConfig", fileName = "AdvertisementConfig")]
    public class AdvertisementConfig : ScriptableObject
    {
        [SerializeField] private float _soulsRewardInterval;
        [SerializeField] private int _heroRevivePerLevel;

        public float SoulsRewardInterval => _soulsRewardInterval;
        public int HeroRevivePerLevel => _heroRevivePerLevel;
    }
}