﻿using UnityEngine;
using System.Collections;


/// <summary>
/// script that resolves (most of) the important game events
/// </summary>
public class GameLogicScripting : MonoBehaviour
{
    void OnAwake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static GameLogicScripting instance = null;


	void OnEnable()
    {
        EventManager.StartListening("BallHitRod", OnBallHitRod);
        EventManager.StartListening("BallHitBlob", OnBallHitBlob);
    }

    void OnDisable()
    {
        EventManager.StopListening("BallHitRod", OnBallHitRod);
        EventManager.StopListening("BallHitBlob", OnBallHitBlob);
    }

    void OnBallHitRod(GameObject g, float f)
    {
        BlobScript.Spawn(BlobScript.GetRandomSpawnPos, g.GetComponent<RodScript>().rodType);
    }

    void OnBallHitBlob(GameObject g, float f)
    {
        GameObject.Destroy(g);
    }


}