﻿using UnityEngine;

namespace Modules.Utils.Runtime.Extensions
{
    public static class SpriteRendererExtensions
    {
        public static void SetAlpha(this SpriteRenderer renderer, float alpha)
        {
            var color = renderer.color;
            color.a = alpha;
            renderer.color = color;
        }
    }
}