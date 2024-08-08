using System.Threading;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.Factory;
using Code.Runtime.Core.LevelLoaderService;
using Code.Runtime.Logic.CameraLogic;
using Code.Runtime.Logic.WaveLogic;
using Code.Runtime.Services;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI;
using Code.Runtime.UI.Services;
using Cysharp.Threading.Tasks;
using GamePush;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints
{
    public class LevelStarterPoint : IAsyncStartable
    {
        private readonly SaveLoadService _saveLoad;
        private readonly HeroFactory _heroFactory;
        private readonly ScreenService _screenService;
        private readonly PlayerControls _controls;
        private readonly EnemySpawnerController _enemySpawnerController;
        private readonly LevelLoader _levelLoader;
        private readonly LevelCollectableTracker _collectableTracker;

        public LevelStarterPoint(
            SaveLoadService saveLoad, HeroFactory heroFactory, 
            ScreenService screenService,
            PlayerControls controls, 
            EnemySpawnerController enemySpawnerController,
            LevelCollectableTracker collectableTracker, 
            LevelLoader levelLoader)
        {
            _saveLoad = saveLoad;
            _heroFactory = heroFactory;
            _screenService = screenService;
            _enemySpawnerController = enemySpawnerController;
            _controls = controls;
            _levelLoader = levelLoader;
            _collectableTracker = collectableTracker;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _saveLoad.Cleanup();

            var config = _levelLoader.GetCurrentLevelConfig();

            await _enemySpawnerController.Initialize(config, cancellation);

            var hero = await InitHero(config);

            InitHud();
            InitializeInput();
            CameraFollow(hero);
            
            _collectableTracker.SetObjectiveExperience(config.ExpForComplete);
            _collectableTracker.SetObjectiveSoulsReward(config.SoulsForComplete);
            _saveLoad.LoadData();
            _controls.UI.Enable();
            _enemySpawnerController.RunSpawner();
            
            GP_Game.GameplayStart();
        }

        private async UniTask<GameObject> InitHero(LevelConfig levelConfig)
        {
            GameObject hero = await _heroFactory.CreateHero(levelConfig.HeroInitialPosition);
            
            return hero;
        }

        private void InitHud()
        {
            _screenService.Open(ScreenID.HUD);
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<MainCameraController>().SetTarget(hero);
        }

        private void InitializeInput()
        {
            _controls.Enable();
        }
    }
}
