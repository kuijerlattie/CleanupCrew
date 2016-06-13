using UnityEngine;
using System.Collections;

public class BreakoutScript : BaseGamestate {

    public float BlobSpawnTime = 10; //time between blob spawns in seconds
    float SpawnTimer = 5;

    public override void StartState()
    {
            
    }

    public override void EndState()
    {

    }

    void Update()
    {
        if (GameManager.instance.CurrentBarrelGoo > 10)
        {
            GameManager.instance.SetState(GameManager.gamestate.BossIntermission);
        }

        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer <= 0)
        {
            //spawn blob from top
            BlobScript.Spawn(BlobScript.GetRandomSpawnPos);
            SpawnTimer = BlobSpawnTime;
        }

    }
}
