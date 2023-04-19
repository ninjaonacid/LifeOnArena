using System.Threading;
using Code.Hero;
using Code.Infrastructure.Factory;
using Code.Logic.EnemySpawners;
using Code.Services;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using Code.StaticData.Levels;
using Code.StaticData.Spawners;
using Code.UI.HUD.Skills;
using Code.UI.HUD;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;
using Code.CameraLogic;

namespace Code.Infrastructure.EntryPoints
{
    public class ArenaStarterPoint : IAsyncStartable
    {
        private readonly IEnemyFactory _enemyFactory;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadService _saveLoad;
        private readonly IProgressService _progress;
        private readonly IHeroFactory _heroFactory;
        private readonly IGameFactory _gameFactory;

        public ArenaStarterPoint(IEnemyFactory enemyFactory,
            IStaticDataService staticData,
            ISaveLoadService saveLoad,
            IProgressService progress, IHeroFactory heroFactory,
            IGameFactory gameFactory)
        {
            _enemyFactory = enemyFactory;
            _staticData = staticData;
            _saveLoad = saveLoad;
            _progress = progress;
            _heroFactory = heroFactory;
            _gameFactory = gameFactory;
        }

        private void InformProgressReaders()
        {
            foreach (ISaveReader progressReader in _saveLoad.ProgressReaders)
                progressReader.LoadProgress(_progress.Progress);
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            LevelConfig config = _staticData.ForLevel(SceneManager.GetActiveScene().name);

            await _enemyFactory.InitAssets();

            await InitSpawners(config);

            var hero = await InitHero(config);

            InitHud(hero);
            CameraFollow(hero);
            InformProgressReaders();
        }

        private async UniTask<GameObject> InitHero(LevelConfig levelConfig)
        {
            GameObject hero = await _heroFactory.CreateHero(levelConfig.HeroInitialPosition);

            return hero;
        }
        private async UniTask InitSpawners(LevelConfig levelConfig)
        {
            foreach (EnemySpawnerData spawnerData in levelConfig.EnemySpawners)
            {
                EnemySpawnPoint spawner = await _enemyFactory.CreateSpawner(
                    spawnerData.Position,
                    spawnerData.Id,
                    spawnerData.MonsterTypeId,
                    spawnerData.RespawnCount);
            }
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
