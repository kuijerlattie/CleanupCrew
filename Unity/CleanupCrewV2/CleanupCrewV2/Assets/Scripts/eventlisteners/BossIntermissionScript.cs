using UnityEngine;
using System.Collections;

public class BossIntermissionScript : BaseGamestate {

    GameObject boss;
    GameObject bosslocation;
    float movespeed = 1f;

    public override void StartState()
    {
        RodScript.DisableAllRods();
        //chose boss to spawn
        bosslocation = GameObject.Find("BossLocation");
        boss = (GameObject)Instantiate(Resources.Load("prefabs/MoleBoss"), bosslocation.transform.position + new Vector3(0, -5, 0), Quaternion.identity);
        

    }

    public override void EndState()
    {

    }

    void Update()
    {
        if (boss.transform.position == bosslocation.transform.position)
        {
            GameManager.instance.SetState(GameManager.gamestate.Boss);
        }
        else
        {
            boss.transform.position = Vector3.MoveTowards(boss.transform.position, bosslocation.transform.position, movespeed * Time.deltaTime);
        }
    }
}
