using BuildingSim.BuildingScene.Items;
using UnityEngine;

namespace BuildingSim.BuildingScene.Controllers
{
    public interface IItemGrid
    {
        Vector3 GetPositionClosestTo(Vector3 position);

        bool PlaceItem(BuildableItem item);

        bool RemoveItem(BuildableItem item);
    }
}