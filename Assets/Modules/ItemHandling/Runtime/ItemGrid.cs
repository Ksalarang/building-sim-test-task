using System;
using Modules.GridModule.Runtime;
using Modules.UIModule.Runtime.Views;
using Modules.UtilsModule.Runtime.Extensions;
using UnityEngine;
using Grid = Modules.GridModule.Runtime.Grid;

namespace Modules.ItemHandling.Runtime
{
    public class ItemGrid
    {
        private readonly Grid _grid;
        private readonly ItemCell[,] _itemCells;

        public ItemGrid(Grid grid, GridConfig gridConfig)
        {
            _grid = grid;
            _itemCells = new ItemCell[gridConfig.GridSize.x, gridConfig.GridSize.y];
        }

        public Vector3 GetPositionClosestTo(Vector3 position)
        {
            return _grid.GetClosestCell(position).WorldPosition;
        }

        public bool PlaceItem(BuildableItem placedItem)
        {
            var closestPosition = _grid.GetClosestCell(placedItem.transform.position).GridPosition;
            var itemCell = GetCell(closestPosition.x, closestPosition.y);

            if (ItemsOverlap(placedItem, itemCell))
            {
                return false;
            }

            ForEachNeighborCell(itemCell, placedItem.GridSize, cell =>
            {
                cell.Item = placedItem;
                cell.Center = itemCell;
            });

            return true;
        }

        public bool RemoveItem(BuildableItem item)
        {
            ItemCell centralCell = null;

            _itemCells.ForEach(cell =>
            {
                if (cell != null && cell.Item == item)
                {
                    centralCell = cell.Center;
                }
            });

            if (centralCell == null)
            {
                return false;
            }

            ForEachNeighborCell(centralCell, item.GridSize, cell =>
            {
                cell.Item = null;
                cell.Center = null;
            });

            return true;
        }

        private bool ItemsOverlap(BuildableItem item, ItemCell itemCell)
        {
            var overlaps = false;

            ForEachNeighborCell(itemCell, item.GridSize, cell =>
            {
                if (cell.Item is not null)
                {
                    overlaps = true;
                }
            });

            return overlaps;
        }

        private void ForEachNeighborCell(ItemCell centralCell, Vector2Int itemGridSize, Action<ItemCell> action)
        {
            var startX = centralCell.X - itemGridSize.x / 2;
            var startY = centralCell.Y - itemGridSize.y / 2;

            for (var x = startX; x < startX + itemGridSize.x; x++)
            {
                for (var y = startY; y < startY + itemGridSize.y; y++)
                {
                    action(GetCell(x, y));
                }
            }
        }

        private ItemCell GetCell(int x, int y)
        {
            var cell = _itemCells[x, y];

            if (cell == null)
            {
                cell = new ItemCell(x, y);
                _itemCells[x, y] = cell;
            }

            return cell;
        }
    }

    public class ItemCell
    {
        public Vector2Int GridPosition;
        public BuildableItem Item;
        public ItemCell Center;

        public int X => GridPosition.x;
        public int Y => GridPosition.y;

        public ItemCell(int x, int y)
        {
            GridPosition = new Vector2Int(x, y);
        }
    }
}