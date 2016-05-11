using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

    public GameObject[] PowerupList;
    float timer = 0;
    public float spawntimerInSeconds = 25;
    public float spawntimerRandomDifference = 10;
    public bool spawnFromCenter = true;

    public enum PowerupType
    {
        MultiPaddle
    }

    // Use this for initialization
    void Start()
    {
        ResetSpawnTimer();
    }

    // Update is called once per frame
    void Update()
    {

        if (timer <= 0)
        {
            SpawnRandomPowerup();
            ResetSpawnTimer();
        }

        timer -= Time.deltaTime;
    }

    void ResetSpawnTimer()
    {
        timer = spawntimerInSeconds + (Random.Range(0, spawntimerRandomDifference * 2) - spawntimerRandomDifference);
    }

    void SpawnRandomPowerup()
    {
        Vector3 spawnlocation = new Vector3(0, 0, 0);
        if (!spawnFromCenter)
        {
            //do other location logic here
        }
        GameObject powerup = (GameObject)Instantiate(PowerupList[Random.Range(0, PowerupList.Length)], spawnlocation, Quaternion.identity);
        //give powerup a outward force;
    }

    public void ActivatePowerup(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.MultiPaddle:
                //activate powerup here.
                
                break;
            default:
                break;
        }
    }

    public void DeactivatePowerup(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.MultiPaddle:
                //deactivate powerup here.
                break;
            default:
                break;
        }
    }

    IEnumerator PowerupTimer(PowerupType type, float durationInSeconds)
    {
        yield return new WaitForSeconds(durationInSeconds);
        DeactivatePowerup(type);
    }
}
