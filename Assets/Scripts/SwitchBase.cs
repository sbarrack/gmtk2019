﻿using UnityEngine;
using UnityEngine.Events;

public class SwitchBase : MonoBehaviour
{
    public bool state = false;
    public UnityEvent switch_enable = new UnityEvent();
    public UnityEvent switch_disable = new UnityEvent();

    protected void Start()
    {
        TileTime.instance.AddListener(OnTick);
    }

    protected void OnTick()
    {
        if (state)
        {
            switch_enable.Invoke();
        }
        else
        {
            switch_disable.Invoke();
        }
    }
}
