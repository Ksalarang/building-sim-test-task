using System;
using Modules.GridModule;
using Modules.UIModule.Runtime.Views;
using Modules.UtilsModule.Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Grid = Modules.GridModule.Grid;

namespace BuildingSim.BuildingScene.Controllers
{
    public class ItemGrid : IItemGrid, IInitializable
    {
        [Inject]
        private readonly Grid _grid;

        [Inject]
        private readonly GridConfig _gridConfig;

        private ItemCell[,] _itemCells;

        public void Initialize()
        {
            _itemCells = new ItemCell[_gridConfig.GridSize.x, _gridConfig.GridSize.y];
        }

        public Vector3 GetPositionClosestTo(Vector3 position)
        {
            return _grid.GetClosestCell(position).WorldPosition;
        }

        public bool PlaceItem(BuildableItem placedItem)
        {
            var cell = _grid.GetClosestCell(placedItem.transform.position);
            var itemCell = GetCell(cell.GridPosition.x, cell.GridPosition.y);

            if (Overlaps(placedItem, itemCell))
            {
                return false;
            }

            ForEachCell(itemCell, placedItem.GridSize, cell =>
            {
                cell.Item = placedItem;
                cell.Center = itemCell;
            });
            return true;
        }

        public bool RemoveItem(BuildableItem itemToRemove)
        {
            ItemCell centralCell = null;

            _itemCells.ForEach(cell =>
            {
                if (cell != null && cell.Item == itemToRemove)
                {
                    centralCell = cell.Center;
                }
            });

            if (centralCell == null)
            {
                return false;
            }

            ForEachCell(centralCell, itemToRemove.GridSize, cell =>
            {
                cell.Item = null;
                cell.Center = null;
            });
            return true;
        }

        private bool Overlaps(BuildableItem item, ItemCell itemCell)
        {
            var overlaps = false;

            ForEachCell(itemCell, item.GridSize, cell =>
            {
                if (cell.Item is not null)
                {
                    overlaps = true;
                }
            });

            return overlaps;
        }

        private void ForEachCell(ItemCell centralCell, Vector2Int itemGridSize, Action<ItemCell> action)
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