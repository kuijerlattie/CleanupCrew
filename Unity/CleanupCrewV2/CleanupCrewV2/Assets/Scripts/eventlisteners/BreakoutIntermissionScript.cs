using UnityEngine;
using System.Collections;

public class BreakoutIntermissionScript : BaseGamestate {

    public override void StartState()
    {
        GameManager.instance.SetState(GameManager.gamestate.Breakout);
    }

    public override void EndState()
    {

    }
}
