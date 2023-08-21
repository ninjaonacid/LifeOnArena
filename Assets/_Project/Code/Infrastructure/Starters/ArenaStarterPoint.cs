using System.Threading;
using Code.Entity.Hero;
using Code.Infrastructure.Factory;
using Code.Infrastructure.InputSystem;
using Code.Logic.CameraLogic;
using Code.Logic.WaveLogic;
using Code.Services;
using Code.Services.PersistentProgress;
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
    public class ArenaStarterPoint : IAsyncStartable
    {
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadService _saveLoad;
        private readonly IGameDataService _gameData;
        private readonly IHeroFactory _heroFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IInputSystem _inputSystem;
        private readonly SpawnerController _spawnerController;
        
        public ArenaStarterPoint(IStaticDataService staticData, 
            ISaveLoadService saveLoad, IGameDataService gameData, 
            IHeroFactory heroFactory, IGameFactory gameFactory,
            IInputSystem inputSystem, SpawnerController spawnerController)
        {
            _staticData = staticData;
            _saveLoad = saveLoad;
            _gameData = gameData;
            _heroFactory = heroFactory;
            _gameFactory = gameFactory;
            _spawnerController = spawnerController;
            _inputSystem = inputSystem;
        }
        
        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _staticData.ForLevel(SceneManager.GetActiveScene().name);

            await _spawnerController.InitSpawners(config, cancellation);

            var hero = await InitHero(config);

            InitHud(hero);
            InitializeInput();
            CameraFollow(hero);

            _saveLoad.LoadSaveData();
            
            _spawnerController.SpawnTimer();
            _spawnerController.RunSpawner();
            
        }

        private async UniTask<GameObject> InitHero(LevelConfig levelConfig)
        {
            GameObject hero = await _heroFactory.CreateHero(levelConfig.HeroInitialPosition);

            return hero;
        }

        private void InitHud(GameObject hero)
        {
            var hud = _gameFactory.CreateHud();

            hud.GetComponentInChildren<ActorUI>().Construct(hero.GetComponent<HeroHealth>());
            hud.GetComponentInChildren<HudSkillContainer>().Construct(hero.GetComponent<HeroSkills>());
        }

        private static void CameraFollow(GameObject hero)
        {
            Camera.main.GetComponent<CameraFollow>().Follow(hero);
        }

        private void InitializeInput()
        {
            _inputSystem.Enable();
        }
    }
}
