using Code.Services;
using Code.StaticData.Levels;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Code.Logic.WaveLogic
{
    public class WaveController
    {
        private readonly IStaticDataService _staticData;
        
        public WaveController(IStaticDataService staticData)
        {
            _staticData = staticData;
            Initialize();
        }

        public void Initialize()
        {
            LevelConfig levelConfig = _staticData.ForLevel(SceneManager.GetActiveScene().ToString());
            string key = levelConfig.LevelKey;
            Debug.Log(key);
        }
    }
}
