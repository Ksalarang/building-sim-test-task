using UnityEngine;

namespace BuildingSim.BuildingScene.Views
{
    public class ItemPanelView : MonoBehaviour
    {
        [field: SerializeField]
        public ItemView[] Items { get; private set; }
    }
}