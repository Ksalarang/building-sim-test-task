using System;
using UnityEngine;

namespace BuildingSim.Input
{
    public interface IMouseInput
    {
        event Action<Vector3> OnClick;

        Vector3 Position { get; }

        bool OverUi { get; }
    }
}