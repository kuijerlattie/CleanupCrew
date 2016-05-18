using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {

    public GameObject[] PowerupList;
    float timer = 0;
    public float spawntimerInSeconds = 25;
    public float spawntimerRandomDifference = 10;
    public bool spawnFromCenter = true;
    public float sizeTimer = 5f;
    public float paddleTimer = 10f;

    [HideInInspector]
    public bool isSpawning = false;

    GameManager manager;

    public enum PowerupType
    {
        MultiPaddle,
        SmallerPaddle,
        BiggerPaddle,
        SlowEnemies,
        MoreEnergy,
        SmallerEnemies,
        Magnetic
    }

    // Use this for initialization
    void Start()
    {
        ResetSpawnTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning) return;
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

    public void SpawnRandomPowerup()
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
                PaddlePowerUp.SpawnPaddles();
                PaddlePowerUp.CalculatePosition();
                StartCoroutine(PowerupTimer(type, paddleTimer));
                break;

            case PowerupType.SmallerPaddle:
                ScalePowerUp.ScaleDown();
                StartCoroutine(PowerupTimer(type, sizeTimer));
                break;

            case PowerupType.BiggerPaddle:
                ScalePowerUp.ScaleUp();
                StartCoroutine(PowerupTimer(type, sizeTimer));
                break;

            case PowerupType.SlowEnemies:
                SlowEnemies.SlowBlobs();
                StartCoroutine(PowerupTimer(type, sizeTimer));
                break;

           case PowerupType.SmallerEnemies:
                SmallerEnemies.SrinkEnemies();
                StartCoroutine(PowerupTimer(type, sizeTimer));
                break;

            case PowerupType.MoreEnergy:
                manager.power += 10;
                break;

            case PowerupType.Magnetic:
                MagneticPaddle.Magnetic();
                StartCoroutine(PowerupTimer(type, sizeTimer));
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
                PaddlePowerUp.RemovePaddle();
                break;

            case PowerupType.SmallerPaddle:
                ScalePowerUp.ScaleUp();
                break;

            case PowerupType.BiggerPaddle:
                ScalePowerUp.ScaleDown();
                break;

            case PowerupType.SlowEnemies:
                SlowEnemies.NormalSpeed();
                break;

            case PowerupType.SmallerEnemies:
                SmallerEnemies.ReturnSize();
                break;

           /* case PowerupType.Magnetic():
                MagneticPaddle.Magnetic();
                break;*/

            default:
                break;
        }
    }

    public void EndAllPowerups()
    {
        StopAllCoroutines();
        foreach (PowerupType p in System.Enum.GetValues(typeof(PowerupType)))
        {
            DeactivatePowerup(p);
        }
    }

    IEnumerator PowerupTimer(PowerupType type, float durationInSeconds)
    {
        yield return new WaitForSeconds(durationInSeconds);
        DeactivatePowerup(type);
    }
}
