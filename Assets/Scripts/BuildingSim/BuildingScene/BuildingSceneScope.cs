using Modules.Grids.Runtime;
using Modules.InputModule.Runtime;
using Modules.ItemHandling.Runtime;
using Modules.UIModule.Runtime.Configs;
using Modules.UIModule.Runtime.Controllers;
using Modules.UIModule.Runtime.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;
using Grid = Modules.Grids.Runtime.Grid;

namespace BuildingSim.BuildingScene
{
    public class BuildingSceneScope : LifetimeScope
    {
        [Header("Scene objects")]
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private GraphicRaycaster _graphicRaycaster;

        [SerializeField]
        private EventSystem _eventSystem;

        [SerializeField]
        private MouseInputHandler _mouseInputHandler;

        [Header("Views")]
        [SerializeField]
        private ItemPanelView _itemPanelView;

        [Header("Configs")]
        [SerializeField]
        private ItemViewsConfig _itemViewsConfig;

        [SerializeField]
        private ItemBuildingConfig _itemBuildingConfig;

        [SerializeField]
        private GridConfig _gridConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_camera);
            builder.RegisterInstance(_graphicRaycaster);
            builder.RegisterInstance(_eventSystem);
            builder.RegisterInstance(_mouseInputHandler).As<IMouseInput>();

            builder.RegisterInstance(_itemPanelView);

            builder.RegisterInstance(_itemViewsConfig);
            builder.RegisterInstance(_itemBuildingConfig);
            builder.RegisterInstance(_gridConfig);

            builder.Register<ItemPanelController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<Grid>(Lifetime.Singleton);
            builder.Register<ItemGrid>(Lifetime.Singleton);
            builder.RegisterEntryPoint<ItemPlacer>();
            builder.RegisterEntryPoint<ItemRemover>();
        }
    }
}