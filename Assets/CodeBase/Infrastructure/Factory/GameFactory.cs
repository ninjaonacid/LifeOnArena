using CodeBase.Infrastructure.AssetManagment;
using CodeBase.Infrastructure.Services.PersistentProgress;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {

        private readonly IAssets _assets;

        public event Action HeroCreated;

        public List<ISavedProgressReader> ProgressReaders { get; } =
            new List<ISavedProgressReader>();

        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject HeroGameObject { get; set; }

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject initialPoint)
        {
            HeroGameObject = InstantiateRegistered(AssetPath.HeroPath,
                                        initialPoint.transform.position);
            HeroCreated?.Invoke();
            return HeroGameObject;
        }

        public void CreateHud() =>
            InstantiateRegistered(AssetPath.HudPath);
        
        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject go = _assets.Instantiate(prefabPath);


            RegisterProgressWatchers(go);
            return go;
        }  
        private GameObject InstantiateRegistered(string prefabPath, Vector3 position)
        {
            GameObject go = _assets.Instantiate(prefabPath, position);


            RegisterProgressWatchers(go);
            return go;
        }

        private void RegisterProgressWatchers(GameObject go)
        {
            foreach (ISavedProgressReader progressReader in
                            go.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }


        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }


    }
    
}