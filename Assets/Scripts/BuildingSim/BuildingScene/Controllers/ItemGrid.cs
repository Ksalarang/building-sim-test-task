using BuildingSim.BuildingScene.Items;
using Modules.GridModule;
using Modules.UtilsModule.Extensions;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace BuildingSim.BuildingScene.Controllers
{
    public class ItemGrid : IItemGrid, IInitializable
    {
        [Inject]
        private readonly IGrid _grid;

        [Inject]
        private readonly GridConfig _gridConfig;

        private BuildableItem[,] _items;

        public void Initialize()
        {
            _items = new BuildableItem[_gridConfig.GridSize.x, _gridConfig.GridSize.y];
        }

        public Vector3 GetPositionClosestTo(Vector3 position)
        {
            return _grid.GetClosestCell(position).WorldPosition;
        }

        public bool PlaceItem(BuildableItem placedItem)
        {
            var cell = _grid.GetClosestCell(placedItem.transform.position);
            var item = _items[cell.GridPosition.x, cell.GridPosition.y];

            if (item is not null)
            {
                return false;
            }

            _items[cell.GridPosition.x, cell.GridPosition.y] = placedItem;
            return true;
        }

        public bool RemoveItem(BuildableItem itemToRemove)
        {
            if (_items.IndicesOf(itemToRemove, out var indices))
            {
                _items[indices.x, indices.y] = null;
                return true;
            }

            return false;
        }
    }
}