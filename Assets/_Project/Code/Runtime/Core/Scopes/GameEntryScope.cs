using Code.Runtime.ConfigData.Identifiers;
using Code.Runtime.Core.EntryPoints.GameEntry;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.Scopes
{
    public class GameEntryScope : LifetimeScope
    {
        [SerializeField] private LevelIdentifier _startLevelId;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();

            builder.Register<GameStateInitializer>(Lifetime.Scoped);
            builder.RegisterInstance(_startLevelId);
        }
    }
}
