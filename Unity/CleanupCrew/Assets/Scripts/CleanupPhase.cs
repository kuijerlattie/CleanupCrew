using UnityEngine;
using System.Collections.Generic;

public class CleanupPhase : AbstractPhase {

    public int SpawnBossAfter = 10;

    //ball specific shit
    public float ballSpawnInterval = 10;
    public float spawnIntervalIncrease = 0.9f;
    public float spawnIntervalPowerIncrease = 0.1f;
    float spawntimer = 0;
    float spawncounter = 0;
    List<GameObject> spheres = new List<GameObject>();

    // Use this for initialization
    public override void StartPhase()
    {
        FindPointZones();
        isActive = true;
    }

    public override void StopPhase()
    {
        isActive = false;
    }

    public override bool HasEnded()
    {
        GameManager manager = GameObject.FindObjectOfType<GameManager>();
        if(manager.pointsWater >= SpawnBossAfter || manager.pointsUnderground >= SpawnBossAfter || manager.pointsSpace >= SpawnBossAfter)
        {
            return true;
        }
        return false;
      
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isActive) return;


        spawntimer -= Time.deltaTime;
        if (spawntimer <= 0)
        {
            spawncounter += spawnIntervalPowerIncrease;
            spawntimer = ballSpawnInterval * Mathf.Pow(0.9f, (float)spawncounter);
            Debug.Log((float)Mathf.Pow(0.9f, (float)spawncounter));
            Vector3 spawnloc = Vector3.zero;
            SpawnSpheres.SpawnSphere(spawnloc);
        }
    }
}
