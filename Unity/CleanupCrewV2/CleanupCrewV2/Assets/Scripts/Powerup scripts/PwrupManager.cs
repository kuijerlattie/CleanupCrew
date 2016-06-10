using UnityEngine;
using System.Collections;

public class PwrupManager : MonoBehaviour {

    public GameObject[] pwrUps;
    public int blobsNeeded = 1; // how many blobs need to be destroyed before spawning pwrup
    public int blobsDestroyed = 0;
    public float durationTimer; // Time how long pwrups stay on

    public enum PowerupType
    {
        smallPaddle,
        bigPaddle
    }

    void BlobDestroyed(GameObject g, float f)
    {
        Debug.Log(true);
        blobsDestroyed += 1;

        if (blobsDestroyed == blobsNeeded)
        {
            SpawnPwrup();
            blobsDestroyed = 0;
        }
    }

    void Start()
    {
        EventManager.StartListening("BlobDestroyed", BlobDestroyed);
    }

    void OnDisable()
    {
        EventManager.StopListening("BlobDestroyed", BlobDestroyed);
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
