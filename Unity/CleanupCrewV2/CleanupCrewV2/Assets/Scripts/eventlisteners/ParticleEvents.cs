using UnityEngine;

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

    }
    void OnBlobHitRod(GameObject g, float f)
    {

    }

    void OnGainedPoints(GameObject g, float f)
    {

    }
    void OnLosePoints(GameObject g, float f)
    {

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
    }
}
