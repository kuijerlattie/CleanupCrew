using UnityEngine;
using System.Collections.Generic;

public class CleanupPhase : AbstractPhase {

    public int SpawnBossAfter = GameSettings.SpawnBossAfterS;

    //ball specific shit
    public float ballSpawnInterval = GameSettings.ballSpawnIntervalS;
    public float spawnIntervalIncrease = GameSettings.spawnIntervalIncreaseS;
    public float spawnIntervalPowerIncrease = GameSettings.spawnIntervalPowerIncreaseS;
    float spawntimer = 0;
    float spawncounter = 0;
    List<GameObject> spheres = new List<GameObject>();

    // Use this for initialization
    public override void StartPhase()
    {
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
        Debug.Log("Water: " + manager.pointsWater + " under: " + manager.pointsUnderground + " space: " + manager.pointsSpace);
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
            spawntimer = ballSpawnInterval * Mathf.Pow(spawnIntervalIncrease, (float)spawncounter);
            Vector3 spawnloc = Vector3.zero;
            SpawnSpheres.SpawnSphere(spawnloc);
        }
    }
}
