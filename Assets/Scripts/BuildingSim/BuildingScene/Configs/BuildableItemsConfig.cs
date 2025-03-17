using System.Linq;
using BuildingSim.BuildingScene.Items;
using UnityEngine;

namespace BuildingSim.BuildingScene.Configs
{
    [CreateAssetMenu(fileName = "BuildableItemsConfig", menuName = "Configs/BuildableItemsConfig", order = 0)]
    public class BuildableItemsConfig : ScriptableObject
    {
        [field: SerializeField]
        public BuildableItem[] Prefabs { get; private set; }

        public BuildableItem GetItem(BuildableItemType type)
        {
            return Prefabs.First(item => item.Type == type);
        }
    }
}