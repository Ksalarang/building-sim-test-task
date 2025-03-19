using UnityEngine;

namespace Modules.Utils.Runtime.Extensions
{
    public static class TransformExtensions
    {
        public static void SetLocalX(this Transform transform, float x)
        {
            var position = transform.localPosition;
            position.x = x;
            transform.localPosition = position;
        }

        public static void SetLocalY(this Transform transform, float y)
        {
            var position = transform.localPosition;
            position.y = y;
            transform.localPosition = position;
        }

        public static void SetLocalZ(this Transform transform, float z)
        {
            var position = transform.localPosition;
            position.z = z;
            transform.localPosition = position;
        }

        public static void SetLocalScaleX(this Transform transform, float x)
        {
            var scale = transform.localScale;
            scale.x = x;
            transform.localScale = scale;
        }

        public static void SetLocalScaleY(this Transform transform, float y)
        {
            var scale = transform.localScale;
            scale.y = y;
            transform.localScale = scale;
        }

        public static void SetLocalScaleZ(this Transform transform, float z)
        {
            var scale = transform.localScale;
            scale.z = z;
            transform.localScale = scale;
        }
    }
}