using UnityEngine;
using System.Collections;

public class pointevents : MonoBehaviour
{


    void OnEnable()
    {
        EventManager.StartListening("BallBottomDeath", OnBallBottomDeath);
        EventManager.StartListening("blobBottomDeath", OnBlobBottomDeath);
        //add all events that you want to listen to when the game starts here.
    }

    void OnDisable()
    {
        EventManager.StopListening("BallBottomDeath", OnBallBottomDeath);
        EventManager.StopListening("blobBottomDeath", OnBlobBottomDeath);
        //add all events to stop listening to here
    }

    void OnBallBottomDeath(GameObject g, float f)
    {
        //remove energy
    }

    void OnBlobBottomDeath(GameObject g, float f)
    {
        //remove energy
    }
}