﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerInteractable : MonoBehaviour
{
	// Start is called before the first frame update
	protected void Start()
    {
		TileTime.instance.AddListener(OnTick);
    }

	// Update is called once per frame
	protected void Update()
    {
        
    }

	protected abstract void OnPlayerInteration();

	protected abstract void OnTick();

}
