﻿using UnityEngine;

public class TestScripting : MonoBehaviour {

	bool waypointIsShown = false;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (SI.idleTime >= 10) SI.SpawnPrefab("Prefabs/Text/MessageAFK", StopIdleMessage);   //TESTING 
        if (SI.idleTime >= 30) FindObjectOfType<GreyboxMenuScript>().StartMenu();
		if (SI.idleTime >= 5 && waypointIsShown == false) {SI.SpawnParticle ("waypoint blink particle 1", Vector3.zero);
			waypointIsShown = true;}
			// needs to be on peddle    


    }

    void OnEnable()
    {
        EventManager.StartListening("BlobDestroyed", OnBlobDestroy);
        EventManager.StartListening("BallDestroyed", OnBallDestroy);
        EventManager.StartListening("EnemyWaterDestroyed", OnEnemyWaterDestroy);
        EventManager.StartListening("EnemyGroundDestroyed", OnEnemyGroundDestroy);
        EventManager.StartListening("EnemySpaceDestroyed", OnEnemySpaceDestroy);
    }

    void OnDisable()
    {
        EventManager.StopListening("BlobDestroyed", OnBlobDestroy);
        EventManager.StopListening("BallDestroyed", OnBallDestroy);
        EventManager.StopListening("EnemyWaterDestroyed", OnEnemyWaterDestroy);
        EventManager.StopListening("EnemyGroundDestroyed", OnEnemyGroundDestroy);
        EventManager.StopListening("EnemySpaceDestroyed", OnEnemySpaceDestroy);
    }

    void OnBlobDestroy(GameObject g, float f)
    {
        SI.SpawnParticle("Fire", g.transform);
    }

    void OnBallDestroy(GameObject g, float f)
    {
        SI.SpawnParticle("STEAM", g.transform);
    }

    void OnEnemyWaterDestroy(GameObject g, float f)
    {
        SI.SpawnParticle("Smoke 2", g.transform);
    }

    void OnEnemyGroundDestroy(GameObject g, float f)
    {
        SI.SpawnParticle("Smoke 2", g.transform);
    }

    void OnEnemySpaceDestroy(GameObject g, float f)
    {
        SI.SpawnParticle("Smoke 2", g.transform);
    }



    bool StopIdleMessage()
    {
        if (SI.idleTimeInt <= 0) return true;
        else return false;
    }



}
