﻿using System.Collections.Generic;
using Modules.UtilsModule.Runtime.Extensions;
using UnityEngine;

namespace Modules.GridModule.Runtime
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

            Debug.LogError($"Position {position} is outside the grid");
            return _cells[_gridConfig.GridSize.x / 2, _gridConfig.GridSize.y / 2];
        }

        public List<Cell> GetCellList()
        {
            var list = new List<Cell>();
            _cells.ForEach(cell => list.Add(cell));
            return list;
        }

        public Cell GetCell(int x, int y)
        {
            return _cells[x, y];
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