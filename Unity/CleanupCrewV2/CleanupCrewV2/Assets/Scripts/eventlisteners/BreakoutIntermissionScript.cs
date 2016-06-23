using UnityEngine;
using System.Collections;

public class BreakoutIntermissionScript : BaseGamestate {

    public override void StartState()
    {
        RodScript.EnableAllRods();
        GameManager.instance.SetState(GameManager.gamestate.Breakout);
        GameManager.instance.EmptyBarrelGoo();
        GameManager.instance.gooNeededForBoss = (int)(4 + (GameManager.instance.CurrentLevel - 1) * 1.25f);
    }

    public override void EndState()
    {

    }
}
