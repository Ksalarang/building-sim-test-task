using UnityEngine;
using UnityEngine.UI;

namespace BuildingSim.BuildingScene.Views
{
    public class ItemPanelView : MonoBehaviour
    {
        [field: SerializeField]
        public ItemView[] Items { get; private set; }

        [field: SerializeField]
        public Button PlaceButton { get; private set; }

        [field: SerializeField]
        public Button RemoveButton { get; private set; }

        [field: SerializeField]
        public Image Frame { get; private set; }
    }
}