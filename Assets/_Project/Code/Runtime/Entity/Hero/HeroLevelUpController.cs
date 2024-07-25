using Code.Runtime.Core.Audio;
using Code.Runtime.Data.PlayerData;
using Code.Runtime.Modules.StatSystem;
using Code.Runtime.Services.PersistentProgress;
using UnityEngine;
using VContainer;

namespace Code.Runtime.Entity.Hero
{
    public class HeroLevelUpController : MonoBehaviour, ISave
    {
        [SerializeField] private HeroStats _stats;
        [SerializeField] private VisualEffectController _vfxController;
        [SerializeField] private FloatingLevel _floatingLevel;
        private AudioService _audioService;
        
        private PlayerData _playerData;
        private int _heroLevel;
        
        [Inject]
        public void Construct(AudioService audioService)
        {
            _audioService = audioService;
        }
        public void LoadData(PlayerData data)
        {
            _playerData = data;
            data.PlayerExp.OnLevelChanged += HandleLevelUp;
        }

        private void HandleLevelUp()
        {
            _floatingLevel.LevelUpLogic();
            _audioService.PlaySound("LevelUp", 1f);
            
            var stats = _stats.Stats;
            
            foreach (var stat in stats.Values)
            {
                if (stat is Health attribute)
                {
                    attribute.Add(attribute.StatPerLevel);
                } 
                else if (stat is PrimaryStat primaryStat)
                {
                    primaryStat.Add(primaryStat.StatPerLevel);
                }
            }
        }

        public void UpdateData(PlayerData data)
        {
           
        }

        private void OnDestroy()
        {
            _playerData.PlayerExp.OnLevelChanged -= HandleLevelUp;
        }
    }
}