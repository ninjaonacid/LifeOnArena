using Code.Runtime.UI.Services;
using VContainer;
using VContainer.Unity;

namespace Code.Runtime.Core.Installers
{
    public class ScreenServiceInstaller : IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ScreenModelFactory>(Lifetime.Scoped);
            builder.Register<ScreenControllerFactory>(Lifetime.Scoped);
            builder.Register<ScreenService>(Lifetime.Scoped);
        }
    }
}