using UnityEngine;
using UnityEngine.UI;

namespace Modules.UIModule.Runtime.Views
{
    public class ItemPanelView : MonoBehaviour
    {
        [field: SerializeField]
        public RectTransform RectTransform { get; private set; }

        [field: SerializeField]
        public Image Frame { get; private set; }

        [field: SerializeField]
        public ItemView[] Items { get; private set; }

        [field: SerializeField]
        public Button PlaceButton { get; private set; }

        [field: SerializeField]
        public Button RemoveButton { get; private set; }
    }
}