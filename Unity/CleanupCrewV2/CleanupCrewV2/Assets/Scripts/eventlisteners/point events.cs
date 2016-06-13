using UnityEngine;
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
        //add all events that you want to listen to when the game starts here.
    }

    void OnDisable()
    {
        EventManager.StopListening("BallBottomDeath", OnBallBottomDeath);
        EventManager.StopListening("BlobBottomDeath", OnBlobBottomDeath);
        EventManager.StopListening("BlobKill", OnBlobKill);
        EventManager.StopListening("BallHitRod", OnRodHit);
        //add all events to stop listening to here
    }

    void OnBallBottomDeath(GameObject g, float f)
    {
        GameManager.instance.LoseEnergy(20);
    }

    void OnBlobBottomDeath(GameObject g, float f)
    {
        GameManager.instance.LoseEnergy(10);
        Destroy(g);
    }

    void OnBlobKill(GameObject g, float f)
    {
        GameManager.instance.AddPoints(20);
        blobcombo++;
    }

    void OnRodHit(GameObject g, float f)
    {
        GameManager.instance.AddPoints(10);
    }

    void OnBallHitPaddle(GameObject g, float f)
    {
        blobcombo = 0;
    }
}