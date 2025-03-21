using System;
using System.Linq;
using Modules.DataPersistence.Runtime;
using Modules.UI.Runtime.Configs;
using Modules.UI.Runtime.Controllers;
using Modules.UI.Runtime.Views;
using Modules.UserInput.Runtime;
using Modules.Utils.Runtime.Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Modules.ItemHandling.Runtime
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
        private readonly ItemGrid _itemGrid;

        [Inject]
        private readonly Camera _camera;

        [Inject]
        private readonly ISaveManager<ItemData> _saveManager;

        private BuildableItem _currentItem;

        public void Initialize()
        {
            _itemPanelView.PlaceButton.onClick.AddListener(OnPlaceButtonClick);
            _itemPanelView.RemoveButton.onClick.AddListener(OnRemoveButtonClick);
            _mouseInput.OnClick += OnClick;

            foreach (var itemView in _itemPanelView.Items)
            {
                itemView.Button.onClick.AddListener(() => OnItemViewClick(itemView));
            }

            LoadItemsFromSave();
        }

        public void Dispose()
        {
            _itemPanelView.PlaceButton.onClick.RemoveListener(OnPlaceButtonClick);
            _itemPanelView.RemoveButton.onClick.RemoveListener(OnRemoveButtonClick);
            _mouseInput.OnClick -= OnClick;

            foreach (var itemView in _itemPanelView.Items)
            {
                itemView.Button.onClick.RemoveAllListeners();
            }
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

            if (ItemOverlapsUi(_currentItem) == false && _itemGrid.PlaceItem(_currentItem))
            {
                _currentItem.Renderer.SetAlpha(1f);
                _currentItem.Renderer.sortingOrder = 0;
                SaveItem(_currentItem, _itemGrid.GetGridPosition(_currentItem.transform.position));
                CreateNewItem(_itemPanelController.SelectedItemType);
            }
        }

        private void CreateNewItem(BuildableItemType type)
        {
            _currentItem = Object.Instantiate(_itemBuildingConfig.GetItem(type));
            _currentItem.Renderer.SetAlpha(_itemBuildingConfig.PrePlacedItemAlpha);
            _currentItem.Renderer.sortingOrder = 10;
            _currentItem.transform.position = _itemGrid.GetPositionClosestTo(_mouseInput.Position);
        }

        private void DestroyCurrentItem()
        {
            Object.Destroy(_currentItem.gameObject);
            _currentItem = null;
        }

        private bool ItemOverlapsUi(BuildableItem item)
        {
            var spriteBounds = new Bounds(item.transform.position, item.Renderer.bounds.size);
            var spriteMin = _camera.WorldToScreenPoint(spriteBounds.min);
            var spriteMax = _camera.WorldToScreenPoint(spriteBounds.max);
            var spriteRect = new Rect(spriteMin, spriteMax - spriteMin);

            var corners = new Vector3[4];
            _itemPanelView.RectTransform.GetWorldCorners(corners);
            // corner values are in screen coordinates because the canvas is set to overlay
            // so no world-to-screen conversion is needed
            var uiRect = new Rect(corners[0], corners[2] - corners[0]);

            return spriteRect.Overlaps(uiRect);
        }

        private void LoadItemsFromSave()
        {
            var itemData = _saveManager.GetData();

            foreach (var gridItem in itemData.Items)
            {
                var item = Object.Instantiate(_itemBuildingConfig.GetItem(gridItem.ItemType));
                item.transform.position = _itemGrid.GetPosition(gridItem.GridPosition);

                if (_itemGrid.PlaceItem(item) == false)
                {
                    Debug.LogError($"Failed to place {gridItem.ItemType} at position {gridItem.GridPosition}");
                }
            }
        }

        private void SaveItem(BuildableItem item, Vector2Int gridPosition)
        {
            var data = _saveManager.GetData();
            var itemData = data.Items.FirstOrDefault(itemData => itemData.GridPosition == gridPosition);

            if (itemData == null)
            {
                data.Items.Add(new GridItem { ItemType = item.Type, GridPosition = gridPosition });
            }
            else
            {
                itemData.ItemType = item.Type;
            }
        }
    }
}