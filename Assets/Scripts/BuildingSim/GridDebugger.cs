using Modules.GridModule;
using Modules.GridModule.Runtime;
using Modules.InputModule.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Grid = Modules.GridModule.Runtime.Grid;

namespace BuildingSim
{
    public class GridDebugger : MonoBehaviour
    {
        [SerializeField]
        private GridConfig _gridConfig;

        [SerializeField]
        private GameObject _cellPrefab;

        [SerializeField]
        private GameObject _pointer;

        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private Transform _cellContainer;

        private InputControls _controls;
        private Grid _grid;

        private void Awake()
        {
            _controls = new InputControls();
            _controls.Enable();
            _controls.Building.MouseClick.performed += OnClick;

            _grid = new Grid(_gridConfig);

            var cells = _grid.GetCellList();
            foreach (var cell in cells)
            {
                var cellObject = Instantiate(_cellPrefab, _cellContainer);
                cellObject.transform.localPosition = cell.WorldPosition;
                cellObject.transform.localScale = new Vector3(_gridConfig.CellLength, _gridConfig.CellLength);
            }
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            var position = _camera.ScreenToWorldPoint(_controls.Building.MousePosition.ReadValue<Vector2>());
            var cell = _grid.GetClosestCell(position);
            _pointer.transform.localPosition = cell.WorldPosition;
        }
    }
}