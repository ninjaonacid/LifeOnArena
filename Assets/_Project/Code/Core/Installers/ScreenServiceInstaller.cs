using Code.UI.Services;
using VContainer;
using VContainer.Unity;

namespace Code.Core.Installers
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