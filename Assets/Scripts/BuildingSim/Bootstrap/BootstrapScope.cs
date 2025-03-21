using Modules.DataPersistence.Runtime;
using Modules.ItemHandling.Runtime;
using VContainer;
using VContainer.Unity;

namespace BuildingSim.Bootstrap
{
    public class BootstrapScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<SaveManager<ItemData>>();
        }
    }
}