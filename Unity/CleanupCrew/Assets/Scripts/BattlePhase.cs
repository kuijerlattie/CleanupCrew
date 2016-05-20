using UnityEngine;
using System.Collections.Generic;
using System;

public class BattlePhase : AbstractPhase
{

    private List<GameObject> enemies = new List<GameObject>();
    // Use this for initialization

    public override void StartPhase()
    {
        isActive = true;

        FindPointZones();
        SetPointZones(true);
        GameManager manager = GameObject.FindObjectOfType<GameManager>();

        PointScript.goalType mostUsedGoal =
            (manager.pointsWater > manager.pointsUnderground && manager.pointsWater > manager.pointsSpace) ? PointScript.goalType.water :
            ((manager.pointsUnderground > manager.pointsWater && manager.pointsUnderground > manager.pointsSpace) ? PointScript.goalType.underground :
            PointScript.goalType.space
            );
        SpawnEnemy(mostUsedGoal);

        //nextGamestate = //TODO back to menu??
    }

    public override void StopPhase()
    {
        isActive = false;
        SetPointZones(false);
        for (int i = enemies.Count -1; i > 0; i--)
        {
            //destroy all enemies that are still alive
            if (enemies[i] != null) GameObject.Destroy(enemies[i]);
        }
    }

    public override bool HasEnded()
    {
        return false;
    }


    public void SpawnEnemy(PointScript.goalType goaltype)
    {
        PointScript currentPscript = null;
        foreach (GameObject pscriptObject in pointZones)
        {
            PointScript pscript = pscriptObject.GetComponent<PointScript>();
            if (pscript.type == goaltype)
            {
                currentPscript = pscript;
                break;
            }
        }
        if (currentPscript == null)
        {
            Debug.Log("WARNING, could not find goal with type: " + goaltype.ToString());
            return;
        }
        Vector3 spawnPos = currentPscript.gameObject.transform.position;
        spawnPos.y = 0;
        Vector3 directionToMiddle = -spawnPos;


        GameObject currentEnemy;
        switch(goaltype)
        {
            case PointScript.goalType.space:
                break;
            case PointScript.goalType.underground:
                break;
            case PointScript.goalType.water:
                break;
        }
        //TODO replace with prefabs in above switch  
        currentEnemy = GameObject.Instantiate(Resources.Load("Prefabs/Blob") as GameObject);
        currentEnemy.layer = LayerMask.NameToLayer("Enemies");
        Rigidbody body = currentEnemy.AddComponent<Rigidbody>();
        body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation;
        body.velocity = directionToMiddle.normalized;
        currentEnemy.AddComponent<FixedSpeed>();

        EnemyScript enemyscript =  currentEnemy.AddComponent<EnemyScript>();
        enemyscript.enemytype = goaltype;

        currentEnemy.transform.position = spawnPos;

        enemies.Add(currentEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

  
    }
}
