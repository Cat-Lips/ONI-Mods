using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyMods;

using static PointerEventData;

public class MyModMouseClick : MonoBehaviour, IPointerClickHandler
{
    public event Action<GameObject, InputButton> Click;

    public void OnPointerClick(PointerEventData e)
    {
        if (e.rawPointerPress == null) return;
        Click?.Invoke(e.rawPointerPress, e.button);
    }
}
