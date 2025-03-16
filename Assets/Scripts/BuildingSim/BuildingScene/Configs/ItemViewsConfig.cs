using System;
using System.Linq;
using BuildingSim.BuildingScene.Items;
using UnityEngine;

namespace BuildingSim.BuildingScene.Configs
{
    [CreateAssetMenu(fileName = "ItemViewsConfig", menuName = "Configs/ItemViewsConfig", order = 0)]
    public class ItemViewsConfig : ScriptableObject
    {
        [field: SerializeField]
        public ItemViewConfig[] ItemViews { get; private set; }

        public Sprite GetSprite(BuildableItemType type)
        {
            return ItemViews.First(config => config.Type == type).Sprite;
        }
    }

    [Serializable]
    public struct ItemViewConfig
    {
        public BuildableItemType Type;
        public Sprite Sprite;
    }
}