using System;
using System.Linq;
using Modules.UI.Runtime.Configs;
using Modules.UI.Runtime.Views;
using VContainer;
using VContainer.Unity;

namespace Modules.UI.Runtime.Controllers
{
    public class ItemPanelController : IItemPanelController, IInitializable, IDisposable
    {
        public BuildableItemType SelectedItemType => _selectedItemType;

        [Inject]
        private readonly ItemPanelView _view;

        [Inject]
        private readonly ItemViewsConfig _itemViewsConfig;

        private BuildableItemType _selectedItemType;

        public void Initialize()
        {
            foreach (var item in _view.Items)
            {
                item.SetSprite(_itemViewsConfig.GetSprite(item.Type));
                item.Button.onClick.AddListener(() => OnItemClick(item));
            }

            var first = _view.Items.First();
            first.Frame.gameObject.SetActive(true);
            _selectedItemType = first.Type;
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
                var selected = item == itemView;
                item.Frame.gameObject.SetActive(selected);

                if (selected)
                {
                    _selectedItemType = item.Type;
                }
            }
        }
    }
}