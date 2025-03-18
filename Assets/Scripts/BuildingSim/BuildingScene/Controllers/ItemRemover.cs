using System;
using BuildingSim.BuildingScene.Items;
using BuildingSim.BuildingScene.Views;
using BuildingSim.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace BuildingSim.BuildingScene.Controllers
{
    public class ItemRemover : IInitializable, IDisposable
    {
        [Inject]
        private readonly ItemPanelView _itemPanelView;

        [Inject]
        private readonly IMouseInput _mouseInput;

        [Inject]
        private readonly IItemGrid _itemGrid;

        private bool _removing;

        public void Initialize()
        {
            _itemPanelView.RemoveButton.onClick.AddListener(OnRemoveButtonClick);
            _itemPanelView.PlaceButton.onClick.AddListener(OnPlaceButtonClick);
            _mouseInput.OnClick += OnClick;
        }

        public void Dispose()
        {
            _itemPanelView.RemoveButton.onClick.RemoveListener(OnRemoveButtonClick);
            _itemPanelView.PlaceButton.onClick.RemoveListener(OnPlaceButtonClick);
            _mouseInput.OnClick -= OnClick;
        }

        private void OnRemoveButtonClick()
        {
            _removing = true;
        }

        private void OnPlaceButtonClick()
        {
            _removing = false;
        }

        private void OnClick(Vector3 position)
        {
            if (_removing == false)
            {
                return;
            }

            var hit = Physics2D.Raycast(_mouseInput.Position, Vector2.zero);

            if (hit.collider && hit.collider.TryGetComponent(out BuildableItem item))
            {
                _itemGrid.RemoveItem(item);
                Object.Destroy(item.gameObject);
            }
        }
    }
}