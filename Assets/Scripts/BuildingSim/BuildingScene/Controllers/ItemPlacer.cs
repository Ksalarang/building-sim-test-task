using System;
using BuildingSim.BuildingScene.Configs;
using BuildingSim.BuildingScene.Items;
using BuildingSim.BuildingScene.Views;
using BuildingSim.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace BuildingSim.BuildingScene.Controllers
{
    public class ItemPlacer : IInitializable, IDisposable, ITickable
    {
        [Inject]
        private readonly ItemPanelView _itemPanelView;

        [Inject]
        private readonly IItemPanelController _itemPanelController;

        [Inject]
        private readonly BuildableItemsConfig _buildableItemsConfig;

        [Inject]
        private readonly IMouseInput _mouseInput;

        [Inject]
        private readonly IItemGrid _itemGrid;

        private BuildableItem _currentItem;

        public void Initialize()
        {
            _itemPanelView.PlaceButton.onClick.AddListener(OnPlaceButtonClick);
            _mouseInput.OnClick += OnClick;
        }

        public void Dispose()
        {
            _itemPanelView.PlaceButton.onClick.RemoveListener(OnPlaceButtonClick);
            _mouseInput.OnClick -= OnClick;
        }

        public void Tick()
        {
            if (_currentItem is null)
            {
                return;
            }

            _currentItem.transform.position = _itemGrid.GetPositionClosestTo(_mouseInput.Position);
        }

        private void OnPlaceButtonClick()
        {
            if (_currentItem is not null)
            {
                return;
            }

            var selectedType = _itemPanelController.SelectedItemType;
            _currentItem = Object.Instantiate(_buildableItemsConfig.GetItem(selectedType));
            _currentItem.transform.position = _itemGrid.GetPositionClosestTo(_mouseInput.Position);
        }

        private void OnClick(Vector3 position)
        {
            if (_currentItem is null || _mouseInput.OverUi)
            {
                return;
            }

            if (_itemGrid.PlaceItem(_currentItem))
            {
                _currentItem = null;
            }
        }
    }
}