using UnityEngine;

namespace BuildingSim.BuildingScene.Items
{
    public class BuildableItem : MonoBehaviour
    {
        [field: SerializeField]
        public BuildableItemType Type { get; private set; }

        [field: SerializeField]
        public SpriteRenderer Renderer { get; private set; }
    }

    public enum BuildableItemType
    {
        RedBuilding,
        BlueBuilding,
        GrayBuilding,
    }
}