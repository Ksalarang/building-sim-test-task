using BuildingSim.BuildingScene.Configs;
using BuildingSim.BuildingScene.Controllers;
using BuildingSim.BuildingScene.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BuildingSim.BuildingScene
{
    public class BuildingSceneScope : LifetimeScope
    {
        [Header("Views")]
        [SerializeField]
        private ItemPanelView _itemPanelView;

        [Header("Configs")]
        [SerializeField]
        private ItemViewsConfig _itemViewsConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_itemPanelView);

            builder.RegisterInstance(_itemViewsConfig);

            builder.RegisterEntryPoint<ItemPanelController>();
        }
    }
}