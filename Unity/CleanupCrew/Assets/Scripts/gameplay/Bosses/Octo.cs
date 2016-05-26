using UnityEngine;
using System.Collections;
using System;

public class Octo : EnemyScript {

    private const int amountOfTentacles = 8;    //TODO put in gamesettings
    GameObject[] tentacles = new GameObject[amountOfTentacles];
    private float tentacleDistance = 10;        //TODO put in gamesettings
    private float rotateDegreesPerSecond = 50;  //TODO put in gamesettings

    // Use this for initialization
    void Start () {
        BaseStart();
        useBaseCollider = false;   
	}

    protected override void SetupWhenReady()
    {
        base.SetupWhenReady();
        for (int i = 0; i < amountOfTentacles; i++)
        {
            tentacles[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            tentacles[i].transform.SetParent(gameObject.transform);
            float calc = Mathf.PI * 2.0f / (float)amountOfTentacles;
            float x = Mathf.Cos(calc * i);  //TODO expensive calcs, tentacles are included in model????
            float z = Mathf.Sin(calc * i);
            tentacles[i].transform.position = new Vector3(x, 0, z) * tentacleDistance;
            tentacles[i].AddComponent<DestroyOnCollision>();
            tentacles[i].GetComponent<Collider>().isTrigger = true;
            //tentacles[i].layer = LayerMask.NameToLayer("Wall");
        }
    }

    protected override bool HasDied()
    {
        //check how many of the tentacles are null or inactive  (destroyed)
        int deadtentacles = Array.FindAll(tentacles, x => x == null || !x.activeSelf).GetLength(0);
        //if they are all dead the boss is dead? 
        if (deadtentacles >= amountOfTentacles && isReady) return true;
        return false;
    }

    // Update is called once per frame
    void Update () {
        gameObject.transform.Rotate(Vector3.up, rotateDegreesPerSecond * Time.deltaTime);
        BaseUpdate();
	}
}
