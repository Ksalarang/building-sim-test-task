using UnityEngine;

namespace Modules.UI.Runtime.Views
{
    public class BuildableItem : MonoBehaviour
    {
        [field: SerializeField]
        public BuildableItemType Type { get; private set; }

        [field: SerializeField]
        public SpriteRenderer Renderer { get; private set; }

        [field: SerializeField]
        public Collider2D Collider { get; private set; }

        [field: SerializeField]
        public Vector2Int GridSize { get; private set; }
    }

    public enum BuildableItemType
    {
        RedBuilding,
        BlueBuilding,
        GrayBuilding,
    }
}