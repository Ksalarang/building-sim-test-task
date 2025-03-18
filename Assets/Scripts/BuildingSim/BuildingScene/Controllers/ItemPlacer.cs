using System;
using BuildingSim.BuildingScene.Configs;
using BuildingSim.BuildingScene.Items;
using BuildingSim.BuildingScene.Views;
using BuildingSim.Input;
using Modules.UtilsModule.Extensions;
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
        private readonly ItemBuildingConfig _itemBuildingConfig;

        [Inject]
        private readonly IMouseInput _mouseInput;

        [Inject]
        private readonly IItemGrid _itemGrid;

        private BuildableItem _currentItem;

        public void Initialize()
        {
            _itemPanelView.PlaceButton.onClick.AddListener(OnPlaceButtonClick);
            _itemPanelView.RemoveButton.onClick.AddListener(OnRemoveButtonClick);

            foreach (var itemView in _itemPanelView.Items)
            {
                itemView.Button.onClick.AddListener(() => OnItemViewClick(itemView));
            }

            _mouseInput.OnClick += OnClick;
        }

        public void Dispose()
        {
            _itemPanelView.PlaceButton.onClick.RemoveListener(OnPlaceButtonClick);
            _itemPanelView.RemoveButton.onClick.RemoveListener(OnRemoveButtonClick);

            foreach (var itemView in _itemPanelView.Items)
            {
                itemView.Button.onClick.RemoveAllListeners();
            }

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

            _itemPanelView.Frame.color = Color.green;
            CreateNewItem(_itemPanelController.SelectedItemType);
        }

        private void OnRemoveButtonClick()
        {
            if (_currentItem is null)
            {
                return;
            }

            DestroyCurrentItem();
        }

        private void OnItemViewClick(ItemView itemView)
        {
            if (_currentItem is null)
            {
                return;
            }

            if (itemView.Type != _currentItem.Type)
            {
                DestroyCurrentItem();
                CreateNewItem(itemView.Type);
            }
        }

        private void OnClick(Vector3 position)
        {
            if (_currentItem is null || _mouseInput.OverUi)
            {
                return;
            }

            if (_itemGrid.PlaceItem(_currentItem))
            {
                _currentItem.Renderer.SetAlpha(1f);
                CreateNewItem(_itemPanelController.SelectedItemType);
            }
        }

        private void CreateNewItem(BuildableItemType type)
        {
            _currentItem = Object.Instantiate(_itemBuildingConfig.GetItem(type));
            _currentItem.Renderer.SetAlpha(_itemBuildingConfig.PrePlacedItemAlpha);
            _currentItem.transform.position = _itemGrid.GetPositionClosestTo(_mouseInput.Position);
        }

        private void DestroyCurrentItem()
        {
            Object.Destroy(_currentItem.gameObject);
            _currentItem = null;
        }
    }
}