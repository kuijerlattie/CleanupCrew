using UnityEngine;
using System.Collections;

public class PwrupManager : MonoBehaviour {

    public GameObject[] pwrUps;
    public int blobsNeeded = 1;
    public int blobsDestroyed = 0;
    public float durationTimer;
    public float spawnTimer = 10;
    public float minSpawnTime = 10, maxSpawnTime = 15;
    public bool bossSpawned = false;
    public bool getWider = false;
    public bool getNarrower = false;
    public bool resetSize = false;

    public static PwrupManager instance = null;
    void OnAwake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    public enum PowerupType
    {
        smallPaddle,
        bigPaddle,
        slowBlobs,
        destroyBlobs,
        bottomShield,
        magneticPaddle,
        shooting
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
        EventManager.StartListening("StartBoss", BossFightStarted);
    }

    void OnDisable()
    {
        EventManager.StopListening("BlobDestroyed", BlobDestroyed);
        EventManager.StopListening("StartBoss", BossFightStarted);
    }
    
    void BossFightStarted(GameObject g, float f)
    {
        bossSpawned = true;
    }

    void Update()
    {
        if(bossSpawned)
        {
            spawnTimer -= Time.deltaTime;
            if(spawnTimer < 0)
            {
                SpawnPwrup();
                spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
            }
        }
    }

    void SpawnPwrup()
    {
        if(GameManager.instance.CurrentGamestate == GameManager.gamestate.Breakout && GameManager.instance.CurrentGameplaystate != GameManager.gameplaystate.paused)
        {
            Vector3 spawnLocation = BlobScript.GetRandomSpawnPos; // current solution, later need to make it spawn where last blob dies(?)
            GameObject pwrUp = (GameObject)Instantiate(pwrUps[Random.Range(0, pwrUps.Length)], spawnLocation, Quaternion.identity);
        }
        
        if(GameManager.instance.CurrentGamestate == GameManager.gamestate.Boss && GameManager.instance.CurrentGameplaystate != GameManager.gameplaystate.paused)
        {
            Vector3 spawnLocation = BlobScript.GetRandomSpawnPos; // current solution, later need to make it spawn where last blob dies(?)
            GameObject powerUp = null;
            while (powerUp == null || (powerUp != null && (powerUp.GetComponent<destroyBlobs>() != null || powerUp.GetComponent<shootingPwrup>() != null || powerUp.GetComponent<SlowPwrup>() != null)))
            {
                powerUp = pwrUps[Random.Range(0, pwrUps.Length)];
            }

            GameObject pwrUp = (GameObject)Instantiate(powerUp, spawnLocation, Quaternion.identity);
        }
    }

    public void ActivatePwrup(PowerupType type)
    {
        switch(type)
        {
            case PowerupType.smallPaddle:
                downScalePwrup.Narrow();
                getNarrower = true;
                StartCoroutine(PwrupTimer(type, durationTimer));
                break;

            case PowerupType.bigPaddle:
                upScalePwrup.Wider();
                getWider = true;
                StartCoroutine(PwrupTimer(type, durationTimer));
                break;

            case PowerupType.slowBlobs:
                SlowPwrup.Slow();
                StartCoroutine(PwrupTimer(type, durationTimer));
                break;

            case PowerupType.destroyBlobs:
                destroyBlobs.destroyAllBlobs();
                break;

            case PowerupType.magneticPaddle:
                magneticPaddle.magnetic();
                break;

            case PowerupType.bottomShield:
                shieldPwrup.activateShield();
                //StartCoroutine(PwrupTimer(type, durationTimer)); atm not needed
                break;

            case PowerupType.shooting:
                shootingPwrup.allowShooting();
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
                getNarrower = false;
                resetSize = true;
                downScalePwrup.ResetScale();
                break;

            case PowerupType.bigPaddle:
                getWider = false;
                resetSize = true;
                upScalePwrup.ResetScale();
                break;

            case PowerupType.slowBlobs:
                SlowPwrup.ResetSpeed();
                break;

         /*   case PowerupType.bottomShield:
                shieldPwrup.deactivateShield();         Atm not in use
                break;
                */
            case PowerupType.shooting:
                shootingPwrup.disallowShooting();
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
