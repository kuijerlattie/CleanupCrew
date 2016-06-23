using UnityEngine;
using System.Collections;

public class AudiEvents : MonoBehaviour {


	void OnBallDestroy (GameObject g, float f)
	{ 
		Debug.Log ("musci played");
		StaticFuntions.PlaySound (g, "balldead").volume = 0.5f;
	}
	void OnBallHitPaddle (GameObject g, float f)
	{
		StaticFuntions.PlaySound (g, "ballhitpaddle").volume = 0.5f;
	}
	void OnBlobBottomDeath (GameObject g, float f)
	{
		StaticFuntions.PlaySound (g, "blolbescaped").volume = 0.5f;
	}
	void OnStartBoss (GameObject g, float f)
	{
		StaticFuntions.PlaySound (g, "molecomesout").volume = 0.5f;
	}
	void OnGameOver (GameObject g, float f)
	{
		StaticFuntions.PlaySound (g, "looose").volume = 0.5f;
	}
	void OnBallHitRod (GameObject g, float f)
	{
		StaticFuntions.PlaySound (g, "powerup").volume = 0.5f;
	}
	void OnBallHitWall(GameObject g, float f)
	{
		StaticFuntions.PlaySound (g, "balhitwall").volume = 1f;
		Debug.Log ("wall is hit");
	}
		

	void OnEnable()
	{
		//EventManager.StartListening("BloanbDestroyed", OnBlobDestroy);
		EventManager.StartListening("BallDestroyed", OnBallDestroy);
		//EventManager.StartListening("BlobSpawn", OnBlobSpawn);
		//EventManager.StartListening("BallSpawn", OnBallSpawn);
		EventManager.StartListening("BallHitRod", OnBallHitRod);
		//EventManager.StartListening("BlobHitRod", OnBlobHitRod);
		EventManager.StartListening("BallHitWall", OnBallHitWall);
		//EventManager.StartListening("GainedPoints", OnGainedPoints);
		//EventManager.StartListening("LosePoints", OnLosePoints);
		//
		EventManager.StartListening("BallHitPaddle", OnBallHitPaddle);
		//EventManager.StartListening("RodMoved", OnRodMoved);
		//EventManager.StartListening("BallShoot", OnBallShoot);
		EventManager.StartListening("StartBoss", OnStartBoss);
		EventManager.StartListening("GameOver", OnGameOver);
		EventManager.StartListening("BlobBottomDeath", OnBlobBottomDeath);
		//
		//EventManager.StartListening("GainedEnergy", OnGainedEnergy);
		//EventManager.StartListening("LoseEnergy", OnLoseEnergy);


	}

	void OnDisable()
	{
		//EventManager.StopListening("BlobDestroyed", OnBlobDestroy);
		EventManager.StopListening("BallDestroyed", OnBallDestroy);
		//EventManager.StopListening("BlobSpawn", OnBlobSpawn);
		//EventManager.StopListening("BallSpawn", OnBallSpawn);
		EventManager.StopListening("BallHitRod", OnBallHitRod);
		//EventManager.StopListening("BlobHitRod", OnBlobHitRod);
		EventManager.StopListening("BallHitWall", OnBallHitWall);
		//EventManager.StopListening("GainedPoints", OnGainedPoints);
		//EventManager.StopListening("LosePoints", OnLosePoints);
		//
		EventManager.StopListening("BallHitPaddle", OnBallHitPaddle);
		//EventManager.StopListening("RodMoved", OnRodMoved);
		//EventManager.StopListening("BallShoot", OnBallShoot);
		EventManager.StopListening("StartBoss", OnStartBoss);
		EventManager.StopListening("GameOver", OnGameOver);
		EventManager.StopListening("BlobBottomDeath", OnBlobBottomDeath);
		//
		//EventManager.StopListening("GainedEnergy", OnGainedEnergy);
		//EventManager.StopListening("LoseEnergy", OnLoseEnergy);
	}

}
