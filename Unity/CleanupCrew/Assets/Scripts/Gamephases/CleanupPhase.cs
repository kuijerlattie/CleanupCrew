using UnityEngine;
using System.Collections.Generic;

public class CleanupPhase : AbstractPhase {

    public int SpawnBossAfter;

    //ball specific shit
    public float ballSpawnInterval;
    public float spawnIntervalIncrease;
    public float spawnIntervalPowerIncrease;
    float spawntimer = 0;
    float spawncounter = 0;
    List<GameObject> spheres = new List<GameObject>();

    // Use this for initialization
    public override void StartPhase()
    {
        SpawnBossAfter = GameSettings.SpawnBossAfterS;
        ballSpawnInterval = GameSettings.ballSpawnIntervalS;
        spawnIntervalIncrease = GameSettings.spawnIntervalIncreaseS;
        spawnIntervalPowerIncrease = GameSettings.spawnIntervalPowerIncreaseS;


        FindPointZones();
        isActive = true;
        nextGamestate = GameManager.gamestate.Battle;
    }

    public override void StopPhase()
    {
        isActive = false;
    }

    public override bool HasEnded()
    {
        GameManager manager = GameObject.FindObjectOfType<GameManager>();
        //Debug.Log("Water: " + manager.pointsWater + " under: " + manager.pointsUnderground + " space: " + manager.pointsSpace);
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
            ballSpawnInterval *= spawnIntervalIncrease;
            spawntimer = ballSpawnInterval; //ballSpawnInterval * Mathf.Pow(spawnIntervalIncrease, (float)spawncounter);
            Vector3 spawnloc = Vector3.zero;
            SpawnSpheres.SpawnSphere(spawnloc);
        }
    }
}
