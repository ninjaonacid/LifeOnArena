using Code.Logic;
using VContainer;
using VContainer.Unity;

namespace Code.Infrastructure.Scopes
{
    public class GameScope : LifetimeScope
    {
        public TestEventLogic eventTester;
        protected override void Configure(IContainerBuilder builder)
        {
           // builder.RegisterComponentInNewPrefab(eventTester, Lifetime.Scoped);
        }
    }
}
