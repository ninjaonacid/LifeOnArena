using System.Threading;
using Code.ConfigData.Levels;
using Code.Core.Factory;
using Code.Core.ObjectPool;
using Code.Logic.CameraLogic;
using Code.Logic.WaveLogic;
using Code.Services;
using Code.Services.ConfigData;
using Code.Services.SaveLoad;
using Code.UI;
using Code.UI.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Core.EntryPoints
{
    public class LevelStarterPoint : IAsyncStartable
    {
        private readonly IConfigProvider _config;
        private readonly ISaveLoadService _saveLoad;
        private readonly IHeroFactory _heroFactory;
        private readonly IScreenService _screenService;
        private readonly PlayerControls _controls;
        private readonly EnemySpawnerController _enemySpawnerController;
        private readonly LevelController _levelController;

        public LevelStarterPoint(IConfigProvider config, 
            ISaveLoadService saveLoad, IHeroFactory heroFactory, IScreenService screenService,
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
