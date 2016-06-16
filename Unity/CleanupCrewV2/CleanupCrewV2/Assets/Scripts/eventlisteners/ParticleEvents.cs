﻿using UnityEngine;

public class ParticleEvents : MonoBehaviour {



    void OnBlobDestroy(GameObject g, float f)
    {
        StaticFuntions.SpawnParticle("blob pop", g.transform.position); //g is in this case the Blob that is destroyed
    }
    void OnBallDestroy(GameObject g, float f)
    {

    }
    void OnBlobSpawn(GameObject g, float f)
    {

    }
    void OnBallSpawn(GameObject g, float f)
    {

    }

    void OnBallHitRod(GameObject g, float f)
    {
		StaticFuntions.SpawnParticle("Thunder", g.transform.position + Vector3.up * 5	); //g is in this case the Blob that is destroyed
    }
    void OnBlobHitRod(GameObject g, float f)
    {

    }

    

    void OnBallHitPaddle(GameObject g, float f)
    {

    }

    void OnRodMoved(GameObject g, float f)
    {
        if(f >= 0)
        {
            //going up

        }
        if (f < 0)
        {
            //going down

        }
    }

    void OnBallShoot(GameObject g, float f)
    {

    }

    void OnStartBoss(GameObject g, float f)
    {
        //g = null, use 'new Vector3(x,y,z)' instead of g.transform.position

    }

    void OnGameOver(GameObject g, float f)
    {
        //g = null, use 'new Vector3(x,y,z)' instead of g.transform.position

    }

    void OnBlobBottomDeath(GameObject g, float f)
    {

    }

    
        

    void OnGainedEnergy(GameObject g, float f)
    {
        //g = null, use 'new Vector3(x,y,z)' instead of g.transform.position

    }

    void OnLoseEnergy(GameObject g, float f)
    {
        //g = null, use 'new Vector3(x,y,z)' instead of g.transform.position

    }


    void OnGainedPoints(GameObject g, float f)
    {
        //g = null, use 'new Vector3(x,y,z)' instead of g.transform.position

    }
    void OnLosePoints(GameObject g, float f)
    {
        //g = null, use 'new Vector3(x,y,z)' instead of g.transform.position

    }







    void OnEnable()
    {
        EventManager.StartListening("BlobDestroyed", OnBlobDestroy);
        EventManager.StartListening("BallDestroyed", OnBallDestroy);
        EventManager.StartListening("BlobSpawn", OnBlobSpawn);
        EventManager.StartListening("BallSpawn", OnBallSpawn);
        EventManager.StartListening("BallHitRod", OnBallHitRod);
        EventManager.StartListening("BlobHitRod", OnBlobHitRod);

        EventManager.StartListening("GainedPoints", OnGainedPoints);
        EventManager.StartListening("LosePoints", OnLosePoints);

        EventManager.StartListening("BallHitPaddle", OnBallHitPaddle);
        EventManager.StartListening("RodMoved", OnRodMoved);
        EventManager.StartListening("BallShoot", OnBallShoot);
        EventManager.StartListening("StartBoss", OnStartBoss);
        EventManager.StartListening("GameOver", OnGameOver);
        EventManager.StartListening("BlobBottomDeath", OnBlobBottomDeath);

        EventManager.StartListening("GainedEnergy", OnGainedEnergy);
        EventManager.StartListening("LoseEnergy", OnLoseEnergy);


    }

    void OnDisable()
    {
        EventManager.StopListening("BlobDestroyed", OnBlobDestroy);
        EventManager.StopListening("BallDestroyed", OnBallDestroy);
        EventManager.StopListening("BlobSpawn", OnBlobSpawn);
        EventManager.StopListening("BallSpawn", OnBallSpawn);
        EventManager.StopListening("BallHitRod", OnBallHitRod);
        EventManager.StopListening("BlobHitRod", OnBlobHitRod);

        EventManager.StopListening("GainedPoints", OnGainedPoints);
        EventManager.StopListening("LosePoints", OnLosePoints);

        EventManager.StopListening("BallHitPaddle", OnBallHitPaddle);
        EventManager.StopListening("RodMoved", OnRodMoved);
        EventManager.StopListening("BallShoot", OnBallShoot);
        EventManager.StopListening("StartBoss", OnStartBoss);
        EventManager.StopListening("GameOver", OnGameOver);
        EventManager.StopListening("BlobBottomDeath", OnBlobBottomDeath);

        EventManager.StopListening("GainedEnergy", OnGainedEnergy);
        EventManager.StopListening("LoseEnergy", OnLoseEnergy);
    }
}
