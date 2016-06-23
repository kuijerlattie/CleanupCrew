﻿using UnityEngine;
using System.Collections;

public class pointevents : MonoBehaviour
{

    int blobcombo = 0;
    void OnEnable()
    {
        EventManager.StartListening("BallBottomDeath", OnBallBottomDeath);
        EventManager.StartListening("BlobBottomDeath", OnBlobBottomDeath);
        EventManager.StartListening("BlobKill", OnBlobKill);
        EventManager.StartListening("BallHitRod", OnRodHit);
        EventManager.StartListening("BallHitPaddle", OnBallHitPaddle);
        EventManager.StartListening("BossHit", OnBossHit);
        EventManager.StartListening("BossDeath", OnBossKill);
        //add all events that you want to listen to when the game starts here.
    }

    void OnDisable()
    {
        EventManager.StopListening("BallBottomDeath", OnBallBottomDeath);
        EventManager.StopListening("BlobBottomDeath", OnBlobBottomDeath);
        EventManager.StopListening("BlobKill", OnBlobKill);
        EventManager.StopListening("BallHitRod", OnRodHit);
        EventManager.StopListening("BossHit", OnBossHit);
        EventManager.StopListening("BossDeath", OnBossKill);
        //add all events to stop listening to here
    }

    void OnBallBottomDeath(GameObject g, float f)
    {
        GameManager.instance.LoseEnergy(20);
    }

    void OnBlobBottomDeath(GameObject g, float f)
    {
        GameManager.instance.LoseEnergy(7);
        Destroy(g);
    }

    void OnBlobKill(GameObject g, float f)
    {
        GameManager.instance.AddPoints(15);
        blobcombo++;
    }

    void OnRodHit(GameObject g, float f)
    {
        GameManager.instance.AddPoints(5);
    }

    void OnBossHit(GameObject g, float f)
    {
        GameManager.instance.AddPoints(5);
    }

    void OnBossKill(GameObject g, float f)
    {
        GameManager.instance.AddPoints(50);
    }

    void OnBallHitPaddle(GameObject g, float f)
    {
        blobcombo = 0;
    }
}