using UnityEngine;
using System.Collections.Generic;
using System;

public class BattlePhase : AbstractPhase
{

    private List<GameObject> enemies = new List<GameObject>();
    private List<GameObject> projectiles = new List<GameObject>();
    private bool hasSpawned = false;

    PointScript.goalType DefaultGoal = PointScript.goalType.water;  //this is what happens if the waste is nicely distrebuted

    public void AddEnemyToList(GameObject go)
    {
        enemies.Add(go);
    }
    public void AddProjectileToList(GameObject go)
    {
        projectiles.Add(go);
    }

    public override void StartPhase()
    {
        isActive = true;

        FindPointZones();
        SetPointZones(true);
        GameManager manager = GameObject.FindObjectOfType<GameManager>();
        
        PointScript.goalType mostUsedGoal =
            (manager.pointsWater > manager.pointsUnderground && manager.pointsWater > manager.pointsSpace) ? PointScript.goalType.water :
            ((manager.pointsUnderground > manager.pointsWater && manager.pointsUnderground > manager.pointsSpace) ? PointScript.goalType.underground :
            ((manager.pointsSpace > manager.pointsWater && manager.pointsSpace > manager.pointsUnderground) ? PointScript.goalType.space : DefaultGoal)
            );
        SpawnEnemy(mostUsedGoal);
        hasSpawned = true;
        nextGamestate = GameManager.gamestate.Cleanup;
        //nextGamestate = //TODO back to menu??
    }

    public override void StopPhase()
    {
        isActive = false;
        SetPointZones(false);
        for (int i = enemies.Count -1; i >= 0; i--)
        {
            //destroy all enemies that are still alive
            if (enemies[i] != null) GameObject.Destroy(enemies[i]);
        }
        for (int i = projectiles.Count-1; i >= 0; i--)
        {
            if (projectiles[i] != null) GameObject.Destroy(projectiles[i]);
        }
        enemies.Clear();
        projectiles.Clear();
    }

    public override bool HasEnded()
    {
        enemies.RemoveAll(a => a == null);
        if (hasSpawned && enemies.Count <= 0)
        {

            //FindObjectOfType<GreyboxMenuScript>().StartMenu();
            //return false; if not going to menu, use return true to start back at tutorial/cleanup depending on 'nextgamestate'
            return true;
            

        }
        return false;
    }


    public GameObject SpawnEnemy(PointScript.goalType goaltype)
    {
        //goaltype = PointScript.goalType.underground;    //TODO remove this, ONLY FOR TESTING
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
            Debug.LogError("Dould not find goal with type: " + goaltype.ToString());
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
            default:
                break;
        }
        
        //TODO proper switch instead of  'goaltype == PointScript.goalType.space ? ... : ... ;' because this is getting horrible to see :D
        string prefabPath = "Prefabs/bosses/";
        prefabPath += goaltype == PointScript.goalType.space ? "BossSpace" : (goaltype == PointScript.goalType.underground ? "BossUnderground" : "BossWater");
        currentEnemy = GameObject.Instantiate(Resources.Load(prefabPath) as GameObject);
        currentEnemy.layer = LayerMask.NameToLayer("Enemies");
        Rigidbody body = currentEnemy.AddComponent<Rigidbody>();
        body.useGravity = false;
        body.constraints = RigidbodyConstraints.FreezeRotation;
        body.velocity = directionToMiddle.normalized;
        currentEnemy.AddComponent<FixedSpeed>();

        switch (goaltype)
        {
            case PointScript.goalType.space:
                currentEnemy.AddComponent<Meteo>().enemytype = goaltype;  //change to space
                break;
            case PointScript.goalType.underground:
                currentEnemy.AddComponent<Mole>().enemytype = goaltype;
                break;
            case PointScript.goalType.water:
                currentEnemy.AddComponent<Octo>().enemytype = goaltype;
                break;
            default:
                break;
        }
        /*
        EnemyScript enemyscript =
            goaltype == PointScript.goalType.space ? 
                currentEnemy.AddComponent<Mole>() : 
            (goaltype == PointScript.goalType.underground? 
                currentEnemy.AddComponent<Mole>() : 
                currentEnemy.AddComponent<Mole>());
                */

        //enemyscript.enemytype = goaltype;

        currentEnemy.transform.position = spawnPos;

        enemies.Add(currentEnemy);  //to check current alive enemies

        return currentEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

  
    }
}
