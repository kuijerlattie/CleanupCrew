using UnityEngine;
using System.Collections;

public class InitializeGame : BaseGamestate {

    GameObject paddle;

    public override void StartState()
    {
        paddle = (GameObject)GameObject.Instantiate(Resources.Load("prefabs/paddle"));
    }

    public override void EndState()
    {
        Destroy(paddle);
    }
}
