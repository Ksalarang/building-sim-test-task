using System.Collections.Generic;
using Modules.Utils.Runtime.Extensions;
using UnityEngine;

namespace Modules.Grids.Runtime
{
    public class Grid
    {
        private readonly GridConfig _gridConfig;
        private readonly Cell[,] _cells;

        public Grid(GridConfig gridConfig)
        {
            _gridConfig = gridConfig;
            _cells = new Cell[gridConfig.GridSize.x, gridConfig.GridSize.y];

            CreateCells();
        }

        public Cell GetClosestCell(Vector2 position)
        {
            var firstCell = _cells[0, 0];
            var gridPosition = (position - firstCell.WorldPosition) / _gridConfig.CellLength;
            var x = Mathf.RoundToInt(gridPosition.x);
            var y = Mathf.RoundToInt(gridPosition.y);

            if (_cells.WithinBounds(x, y))
            {
                return _cells[x, y];
            }

            //todo:
            // Debug.LogError($"Position {position} is outside the grid");
            return _cells[_gridConfig.GridSize.x / 2, _gridConfig.GridSize.y / 2];
        }

        public List<Cell> GetCellList()
        {
            var list = new List<Cell>();
            _cells.ForEach(cell => list.Add(cell));
            return list;
        }

        public bool TryGetCell(int x, int y, out Cell cell)
        {
            if (_cells.WithinBounds(x, y))
            {
                cell = _cells[x, y];
                return true;
            }

            cell = new Cell();
            return false;
        }

        private void CreateCells()
        {
            var halfCellLength = _gridConfig.CellLength / 2;
            var firstCellPosition = new Vector2(
                _gridConfig.GridPosition.x - (_gridConfig.GridSize.x - 1) * halfCellLength,
                _gridConfig.GridPosition.y - (_gridConfig.GridSize.y - 1) * halfCellLength);

            for (var x = 0; x < _gridConfig.GridSize.x; x++)
            {
                for (var y = 0; y < _gridConfig.GridSize.y; y++)
                {
                    var position = new Vector2(firstCellPosition.x + x * _gridConfig.CellLength,
                        firstCellPosition.y + y * _gridConfig.CellLength);

                    _cells[x, y] = new Cell(new Vector2Int(x, y), position);
                }
            }
        }
    }
}