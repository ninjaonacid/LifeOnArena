using Code.Data;
using Code.Services.PersistentProgress;
using Code.StaticData.StatSystem;
using UnityEngine;

namespace Code.Hero
{
    public class HeroStats : StatController, ISave
    {
        private CharacterStatsData _characterStatsData;
        public void LoadProgress(PlayerProgress progress)
        {
            _characterStatsData = progress.CharacterStatsData;

            
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            throw new System.NotImplementedException();
        }
    }
}
