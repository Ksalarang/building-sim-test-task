using System.Linq;
using BuildingSim.BuildingScene.Items;
using UnityEngine;

namespace BuildingSim.BuildingScene.Configs
{
    [CreateAssetMenu(fileName = "ItemBuildingConfig", menuName = "Configs/ItemBuildingConfig", order = 0)]
    public class ItemBuildingConfig : ScriptableObject
    {
        [field: SerializeField]
        public BuildableItem[] Prefabs { get; private set; }

        [field: SerializeField, Tooltip("Transparency of the item that is being placed")]
        public float PrePlacedItemAlpha { get; private set; }

        public BuildableItem GetItem(BuildableItemType type)
        {
            return Prefabs.First(item => item.Type == type);
        }
    }
}