using System.Threading;
using Code.Hero;
using Code.Infrastructure.Factory;
using Code.Infrastructure.SceneManagement;
using Code.Logic;
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
        private readonly IProgressService _progress;
        private readonly IHeroFactory _heroFactory;
        private readonly IGameFactory _gameFactory;
        private readonly SpawnerController _spawnerController;
        public ArenaStarterPoint(IStaticDataService staticData, 
            ISaveLoadService saveLoad, IProgressService progress, 
            IHeroFactory heroFactory, IGameFactory gameFactory, 
            SpawnerController spawnerController)
        {
            _staticData = staticData;
            _saveLoad = saveLoad;
            _progress = progress;
            _heroFactory = heroFactory;
            _gameFactory = gameFactory;
            _spawnerController = spawnerController;
        }

        private void InformProgressReaders()
        {
            foreach (ISaveReader progressReader in _saveLoad.ProgressReaders)
                progressReader.LoadProgress(_progress.Progress);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _staticData.ForLevel(SceneManager.GetActiveScene().name);

            await _spawnerController.InitSpawners(config, cancellation);

            var hero = await InitHero(config);

            InitHud(hero);
            CameraFollow(hero);
            InformProgressReaders();

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
    }
}
