using System.Linq;
using Modules.UIModule.Runtime.Views;
using UnityEngine;

namespace Modules.UIModule.Runtime.Configs
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