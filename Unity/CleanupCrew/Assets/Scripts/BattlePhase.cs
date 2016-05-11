using UnityEngine;
using System.Collections.Generic;

public class BattlePhase : AbstractPhase
{

    private PointScript[] pointAreas;
    private List<GameObject> enemies = new List<GameObject>();
    // Use this for initialization

    public override void StartPhase()
    {
        Debug.Log("START");
        isActive = true;
        FindPointAreas();
        PointScript.goalType mostUsedGoal = PointScript.goalType.space; //TODO, not tracking most used yet.
        SpawnEnemy(mostUsedGoal);
    }

    public override void StopPhase()
    {
        isActive = false;
        for (int i = enemies.Count -1; i > 0; i--)
        {
            //destroy all enemies that are still alive
            if (enemies[i] != null) GameObject.Destroy(enemies[i]);
        }
    }

    private void FindPointAreas()
    {
        pointAreas = GameObject.FindObjectsOfType<PointScript>();
        if (pointAreas.GetLength(0) < 3) Debug.Log("WARNING, less then 3 'PointScripts'");
    }

    public void SpawnEnemy(PointScript.goalType goaltype)
    {
        PointScript currentPscript = null;
        foreach (PointScript pscript in pointAreas)
        {
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


        //TODO actual spawning
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
        //currentEnemy = GameObject.CreatePrimitive(PrimitiveType.Cube);  //TODO replace with prefabs in above switch 
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
