using System.Threading;
using Code.Entity.Hero;
using Code.Infrastructure.Factory;
using Code.Logic.CameraLogic;
using Code.Logic.WaveLogic;
using Code.Services;
using Code.Services.SaveLoad;
using Code.StaticData.Levels;
using Code.UI.HUD;
using Code.UI.HUD.Skills;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Code.Infrastructure.Starters
{
    public class LevelStarterPoint : IAsyncStartable
    {
        private readonly IConfigDataProvider _configData;
        private readonly ISaveLoadService _saveLoad;
        private readonly IHeroFactory _heroFactory;
        private readonly IGameFactory _gameFactory;
        private readonly PlayerControls _controls;
        private readonly EnemySpawnerController _enemySpawnerController;
        private readonly LevelController _levelController;
        
        public LevelStarterPoint(IConfigDataProvider configData, 
            ISaveLoadService saveLoad, IHeroFactory heroFactory, IGameFactory gameFactory,
            PlayerControls controls, EnemySpawnerController enemySpawnerController,
            LevelController controller)
        {
            _configData = configData;
            _saveLoad = saveLoad;
            _heroFactory = heroFactory;
            _gameFactory = gameFactory;
            _enemySpawnerController = enemySpawnerController;
            _controls = controls;
            _levelController = controller;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _configData.ForLevel(SceneManager.GetActiveScene().name);

            await _enemySpawnerController.InitSpawners(config, cancellation);

            var hero = await InitHero(config);

            InitHud(hero);
            InitializeInput();
            CameraFollow(hero);

            _saveLoad.LoadSaveData();
            
            _levelController.Subscribe();
            
            _enemySpawnerController.SpawnTimer();
            _enemySpawnerController.RunSpawner();
            
        }

        private async UniTask<GameObject> InitHero(LevelConfig levelConfig)
        {
            GameObject hero = await _heroFactory.CreateHero(levelConfig.HeroInitialPosition);

            return hero;
        }

        private void InitHud(GameObject hero)
        {
            var hud = _gameFactory.CreateHud();

            var heroHealth = hero.GetComponent<HeroHealth>();
            var heroSkills = hero.GetComponent<HeroSkills>();
            var heroAttack = hero.GetComponent<HeroAttack>();

            hud.GetComponentInChildren<ActorUI>().Construct(heroHealth);
            hud.GetComponentInChildren<HudSkillContainer>().Construct(heroSkills);
            hud.GetComponentInChildren<ComboCounter>().Construct(heroAttack, heroHealth);
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
