﻿using UnityEngine;
using System.Collections;

public class menustartgame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel(1);
        }
	}
}