using Modules.GridModule;
using UnityEngine;
using UnityEngine.InputSystem;
using Grid = Modules.GridModule.Grid;

namespace BuildingSim
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private GridConfig _gridConfig;

        [SerializeField]
        private GameObject _cellPrefab;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private GameObject _circle;

        [SerializeField]
        private Transform _cellContainer;

        private Controls _controls;
        private Grid _grid;

        private void Awake()
        {
            _controls = new Controls();
            _controls.Enable();
            _controls.Building.MouseClick.performed += OnClick;

            _grid = new Grid(_gridConfig);

            var cells = _grid.GetCellList();
            foreach (var cell in cells)
            {
                var cellObject = Instantiate(_cellPrefab, _cellContainer);
                cellObject.transform.localPosition = cell.WorldPosition;
            }
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            var position = _camera.ScreenToWorldPoint(_controls.Building.MousePosition.ReadValue<Vector2>());
            var cell = _grid.GetClosestCell(position);
            _circle.transform.localPosition = cell.WorldPosition;
            Debug.Log($"closest: {cell}");
        }
    }
}