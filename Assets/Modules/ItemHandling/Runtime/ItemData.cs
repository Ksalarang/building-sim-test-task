using System;
using System.Collections.Generic;
using Modules.UI.Runtime.Views;
using UnityEngine;

namespace Modules.ItemHandling.Runtime
{
    [Serializable]
    public class ItemData
    {
        public List<GridItem> Items = new();
    }

    [Serializable]
    public class GridItem
    {
        public BuildableItemType ItemType;
        public Vector2Int GridPosition;
    }
}