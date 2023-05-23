using System;
using UnityEngine;

namespace MyMods;

public class MyModKeyPress : MonoBehaviour
{
    private KeyCode[] keys;

    public event Action<GameObject, KeyCode> KeyDown;
    public event Action<GameObject, KeyCode> KeyUp;
    public event Action<GameObject, KeyCode> Key;

    public MyModKeyPress Keys(params KeyCode[] keys)
    {
        this.keys = keys;
        return this;
    }

    private void Update()
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key)) KeyDown?.Invoke(gameObject, key);
            if (Input.GetKeyUp(key)) KeyUp?.Invoke(gameObject, key);
            if (Input.GetKey(key)) Key?.Invoke(gameObject, key);
        }
    }
}
