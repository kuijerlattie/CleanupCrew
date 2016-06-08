using UnityEngine;
using System.Collections;
using System;

public class IntroScript : BaseGamestate {

    // Use this for initialization

    public override void StartState()
    {
        GameManager.instance.SetState(GameManager.gamestate.Tutorial);
    }

    public override void EndState()
    {
        
    }
}
