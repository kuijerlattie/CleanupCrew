using UnityEngine;
using System.Collections;

public class BossIntermissionScript : BaseGamestate {

    GameObject boss;
    GameObject bosslocation;
    float movespeed = 1f;
    float t = 0f;

    public override void StartState()
    {
        BumperScript.RemoveBumpers();
        CameraShake.ScreenShake(4, 0.3f);
        //destroy all blobs
        BlobScript[] blobs = FindObjectsOfType<BlobScript>();

        for (int i = 0; i < blobs.Length; i++)
        {
            Destroy(blobs[i].gameObject);
        }

        RodScript.DisableAllRods();
        //chose boss to spawn
        bosslocation = GameObject.Find("BossLocation");
        boss = (GameObject)Instantiate(Resources.Load("prefabs/MoleBoss"), bosslocation.transform.position + new Vector3(0, 0, 0), Quaternion.identity);
        boss.GetComponent<BossBase>().maxhitpoints = (int)( 4 + (GameManager.instance.CurrentLevel-1)*1.25f);
        boss.GetComponent<BossBase>().hitpoints = (int)( 4 + (GameManager.instance.CurrentLevel-1)*1.25f);

        FindObjectOfType<PaddleControls>().ResetBall();
    }

    public override void EndState()
    {

    }

    void Update()
    {
        t += Time.deltaTime;
        if (t >= 5f)
        {
            GameManager.instance.SetState(GameManager.gamestate.Boss);
        }
    }
}
