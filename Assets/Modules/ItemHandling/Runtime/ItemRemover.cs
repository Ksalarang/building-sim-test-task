using System;
using Modules.UI.Runtime.Views;
using Modules.UserInput.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Modules.ItemHandling.Runtime
{
    public class ItemRemover : IInitializable, IDisposable
    {
        [Inject]
        private readonly ItemPanelView _itemPanelView;

        [Inject]
        private readonly IMouseInput _mouseInput;

        [Inject]
        private readonly ItemGrid _itemGrid;

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
            _itemPanelView.Frame.color = Color.red;
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
                if (_itemGrid.RemoveItem(item))
                {
                    Object.Destroy(item.gameObject);
                }
            }
        }
    }
}