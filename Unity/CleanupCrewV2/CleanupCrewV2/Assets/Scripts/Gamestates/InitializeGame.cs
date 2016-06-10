using UnityEngine;
using System.Collections;

public class InitializeGame : BaseGamestate {

    GameObject paddle;

    public override void StartState()
    {
        paddle = (GameObject)GameObject.Instantiate(Resources.Load("prefabs/paddle"));
        GameManager.instance.SetEnergy(100);
        GameManager.instance.SetPoints(0);
    }

    public override void EndState()
    {
        Destroy(paddle);
    }
}
