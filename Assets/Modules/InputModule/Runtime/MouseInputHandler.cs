using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using VContainer;

namespace Modules.InputModule.Runtime
{
    public class MouseInputHandler : MonoBehaviour, IMouseInput
    {
        public event Action<Vector3> OnClick;
        public Vector3 Position => _mousePosition;
        public bool OverUi => IsOverUi();

        [Inject]
        private readonly Camera _camera;

        [Inject]
        private readonly EventSystem _eventSystem;

        [Inject]
        private readonly GraphicRaycaster _graphicRaycaster;

        private readonly List<RaycastResult> _raycastResults = new();

        private InputControls _controls;
        private Vector3 _mousePosition;

        private void Awake()
        {
            _controls = new InputControls();
        }

        private void OnEnable()
        {
            _controls.Enable();
            _controls.Building.MouseClick.performed += OnClickPerformed;
        }

        private void OnDisable()
        {
            _controls.Disable();
            _controls.Building.MouseClick.performed -= OnClickPerformed;
        }

        private void Update()
        {
            _mousePosition = GetMousePosition();
            _mousePosition.z = 0;
        }

        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            OnClick?.Invoke(GetMousePosition());
        }

        private bool IsOverUi()
        {
            var eventData = new PointerEventData(_eventSystem);
            eventData.position = _controls.Building.MousePosition.ReadValue<Vector2>();
            _raycastResults.Clear();
            _graphicRaycaster.Raycast(eventData, _raycastResults);

            return _raycastResults.Count > 0;
        }

        private Vector3 GetMousePosition()
        {
            return _camera.ScreenToWorldPoint(_controls.Building.MousePosition.ReadValue<Vector2>());
        }
    }
}