using UnityEngine;
using System.Collections;

public class BreakoutIntermissionScript : BaseGamestate {

    public override void StartState()
    {
        RodScript.EnableAllRods();
        GameManager.instance.SetState(GameManager.gamestate.Breakout);
        GameManager.instance.EmptyBarrelGoo();
    }

    public override void EndState()
    {

    }
}
