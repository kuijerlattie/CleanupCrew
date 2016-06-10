using UnityEngine;
using System.Collections;

public class PwrupManager : MonoBehaviour {

    public GameObject[] pwrUps;
    public int blobsNeeded = 3; // how many blobs need to be destroyed before spawning pwrup
    public float durationTimer; // Time how long pwrups stay on

    public enum PowerupType
    {
        smallPaddle,
        bigPaddle
    }

    void OnEnable()
    {
       // EventManager.StartListening();
    }

    void OnDisable()
    {

    }

    void SpawnPwrup()
    {
        Vector3 spawnLocation = new Vector3(0, 0, 0); // current solution, later need to make it spawn where last blob dies(?)
        GameObject pwrUp = (GameObject)Instantiate(pwrUps[Random.Range(0, pwrUps.Length)], spawnLocation, Quaternion.identity);
        
    }

    public void ActivatePwrup(PowerupType type)
    {
        switch(type)
        {
            case PowerupType.smallPaddle:
                downScalePwrup.Narrow();
                StartCoroutine(PwrupTimer(type, durationTimer));
                break;

            case PowerupType.bigPaddle:
                upScalePwrup.Wider();
                StartCoroutine(PwrupTimer(type, durationTimer));
                break;

            default:
                break;
        }
    }

    public void DeactivatePwrup(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.smallPaddle:
                downScalePwrup.ResetScale();
                break;

            case PowerupType.bigPaddle:
                upScalePwrup.ResetScale();
                break;

            default:
                break;
        }
    }



    IEnumerator PwrupTimer(PowerupType type, float durationInSeconds)
    {
        yield return new WaitForSeconds(durationInSeconds);
        DeactivatePwrup(type);
    }
}
