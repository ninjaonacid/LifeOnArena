using Code.Runtime.UI.Services;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.Installers
{
    public class ScreenServiceInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<IScreenModelFactory, ScreenModelFactory>(Lifetime.Scoped);
            builder.Register<IScreenControllerFactory, ScreenControllerFactory>(Lifetime.Scoped);
            builder.Register<IScreenService, ScreenService>(Lifetime.Scoped);
        }
    }
}