﻿using UnityEngine;
using System.Collections;

public class BumperScript : MonoBehaviour {

    private static bool _addedBumpers = false;
    public static void AddBumpers()
    {
        if (_addedBumpers) return;
        GameObject.Instantiate(Resources.Load("prefabs/Bumpers"));
        _addedBumpers = true;
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (GameManager.instance.CurrentGamestate != GameManager.gamestate.BossIntermission)
            {
                //make ball bounce off 4x the speed
                col.gameObject.GetComponent<Rigidbody>().velocity *= 4;
                col.gameObject.GetComponent<FixedSpeed>().SlowResetSpeed(); //to make sure the speed drops off quickly after the speedup
                EventManager.TriggerEvent("BallHitRod", this.gameObject);
            }
        }

        if (col.gameObject.tag == "Blob")
        {
            EventManager.TriggerEvent("BlobHitRod", col.gameObject);
        }
    }
}
