using UnityEngine;
using System.Collections;

public class BreakoutIntermissionScript : BaseGamestate {

    public override void StartState()
    {
        RodScript.EnableAllRods();
        GameManager.instance.SetState(GameManager.gamestate.Breakout);
    }

    public override void EndState()
    {

    }
}
