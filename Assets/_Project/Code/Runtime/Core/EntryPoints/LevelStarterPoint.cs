using System.Threading;
using Code.Runtime.ConfigData.Levels;
using Code.Runtime.Core.Config;
using Code.Runtime.Core.Factory;
using Code.Runtime.Logic.CameraLogic;
using Code.Runtime.Logic.WaveLogic;
using Code.Runtime.Services;
using Code.Runtime.Services.SaveLoad;
using Code.Runtime.UI;
using Code.Runtime.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Runtime.Core.EntryPoints
{
    public class LevelStarterPoint : IAsyncStartable
    {
        private readonly ConfigProvider _config;
        private readonly ISaveLoadService _saveLoad;
        private readonly IHeroFactory _heroFactory;
        private readonly ScreenService _screenService;
        private readonly PlayerControls _controls;
        private readonly EnemySpawnerController _enemySpawnerController;
        private readonly LevelController _levelController;

        public LevelStarterPoint(ConfigProvider config, 
            ISaveLoadService saveLoad, IHeroFactory heroFactory, ScreenService screenService,
            PlayerControls controls, EnemySpawnerController enemySpawnerController,
            LevelController controller)
        {
            _config = config;
            _saveLoad = saveLoad;
            _heroFactory = heroFactory;
            _screenService = screenService;
            _enemySpawnerController = enemySpawnerController;
            _controls = controls;
            _levelController = controller;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            _saveLoad.Cleanup();

            LevelConfig config = _config.Level(SceneManager.GetActiveScene().name);

            await _enemySpawnerController.InitSpawners(config, cancellation);

            var hero = await InitHero(config);

            InitHud();
            InitializeInput();
            CameraFollow(hero);

            _saveLoad.LoadData();
            
            _levelController.Subscribe();
            
            _enemySpawnerController.SpawnTimer();
            _enemySpawnerController.RunSpawner();
            
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
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }

        private void InitializeInput()
        {
            _controls.Enable();
        }
    }
}
