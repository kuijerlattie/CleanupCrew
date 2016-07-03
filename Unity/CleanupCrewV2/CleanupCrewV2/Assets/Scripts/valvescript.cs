﻿using UnityEngine;
using System.Collections;

public class valvescript : MonoBehaviour {

    // Use this for initialization
    public bool activated = false;
    public int valveId;

    void OnCollisionEnter(Collision col)
    {
        EventManager.TriggerEvent("ValveAlwaysHit", col.gameObject, valveId);
        if (GameManager.instance.CurrentGamestate == GameManager.gamestate.Tutorial || GameManager.instance.CurrentGamestate == GameManager.gamestate.Boss) return;
        if (col.gameObject.tag == "Ball" && !activated)
        {
            activated = true;
            SetOutlineColor(Color.green);
            EventManager.TriggerEvent("ValveHit", col.gameObject, valveId);
        }
    }


    public void SetOutlineColor(Color c)
    {
        //if(gameObject.GetComponent<Renderer>().materials.GetLength(0) < 2) return;
        if (gameObject.GetComponent<Renderer>().materials.GetLength(0) > 1)
            gameObject.GetComponent<Renderer>().materials[1].SetColor("_OutlineColor", c);

    }
}
