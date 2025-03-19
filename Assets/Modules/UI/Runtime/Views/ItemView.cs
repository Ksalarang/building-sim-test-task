using Modules.Utils.Runtime.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.UI.Runtime.Views
{
    public class ItemView : MonoBehaviour
    {
        [field: SerializeField]
        public BuildableItemType Type { get; private set; }

        [field: SerializeField]
        public Image Image { get; private set; }

        [field: SerializeField]
        public Image Frame { get; private set; }

        [field: SerializeField]
        public Button Button { get; private set; }

        public void SetSprite(Sprite sprite)
        {
            PreserveSpriteAspectRatio(sprite);
            Image.sprite = sprite;
        }

        private void PreserveSpriteAspectRatio(Sprite sprite)
        {
            var size = sprite.rect.size;

            if (size.x < size.y)
            {
                Image.transform.SetLocalScaleX((size.x / size.y) * Image.transform.localScale.y);
            }
            else
            {
                Image.transform.SetLocalScaleY((size.y / size.x) * Image.transform.localScale.x);
            }
        }
    }
}