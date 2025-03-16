using System;
using System.Linq;
using BuildingSim.BuildingScene.Configs;
using BuildingSim.BuildingScene.Views;
using VContainer;
using VContainer.Unity;

namespace BuildingSim.BuildingScene.Controllers
{
    public class ItemPanelController : IInitializable, IDisposable
    {
        [Inject]
        private readonly ItemPanelView _view;

        [Inject]
        private readonly ItemViewsConfig _itemViewsConfig;

        public void Initialize()
        {
            foreach (var item in _view.Items)
            {
                item.SetSprite(_itemViewsConfig.GetSprite(item.Type));
                item.Button.onClick.AddListener(() => OnItemClick(item));
            }

            _view.Items.First().Frame.gameObject.SetActive(true);
        }

        public void Dispose()
        {
            foreach (var item in _view.Items)
            {
                item.Button.onClick.RemoveAllListeners();
            }
        }

        private void OnItemClick(ItemView itemView)
        {
            foreach (var item in _view.Items)
            {
                item.Frame.gameObject.SetActive(item == itemView);
            }
        }
    }
}