using System;
using System.Linq;
using Modules.UIModule.Runtime.Views;
using UnityEngine;

namespace Modules.UIModule.Runtime.Configs
{
    [CreateAssetMenu(fileName = "ItemViewsConfig", menuName = "Modules/UIModule/ItemViewsConfig", order = 0)]
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