﻿using UnityEngine;
using System.Collections;

public class BossIntermissionScript : BaseGamestate {

    GameObject boss;
    GameObject bosslocation;
    float movespeed = 1f;

    public override void StartState()
    {
        //destroy all blobs
        BlobScript[] blobs = FindObjectsOfType<BlobScript>();

        for (int i = 0; i < blobs.Length; i++)
        {
            Destroy(blobs[i].gameObject);
        }

        RodScript.DisableAllRods();
        //chose boss to spawn
        bosslocation = GameObject.Find("BossLocation");
        boss = (GameObject)Instantiate(Resources.Load("prefabs/MoleBoss"), bosslocation.transform.position + new Vector3(0, -5, 0), Quaternion.identity);
        boss.GetComponent<BossBase>().maxhitpoints = 5;
        boss.GetComponent<BossBase>().hitpoints = 5;

    }

    public override void EndState()
    {

    }

    void Update()
    {
        if (boss.transform.position == bosslocation.transform.position)
        {
            GameManager.instance.SetState(GameManager.gamestate.Boss);
        }
        else
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, bosslocation.transform.position, movespeed * Time.deltaTime);
        }
    }
}
