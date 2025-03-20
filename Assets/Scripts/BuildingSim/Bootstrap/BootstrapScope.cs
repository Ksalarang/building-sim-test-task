using Modules.DataPersistence.Runtime;
using VContainer;
using VContainer.Unity;

namespace BuildingSim.Bootstrap
{
    public class BootstrapScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // builder.RegisterEntryPoint<SaveManager>();
        }
    }
}