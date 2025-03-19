using System;
using UnityEngine;

namespace Modules.UserInput.Runtime
{
    public interface IMouseInput
    {
        event Action<Vector3> OnClick;

        Vector3 Position { get; }

        bool OverUi { get; }
    }
}