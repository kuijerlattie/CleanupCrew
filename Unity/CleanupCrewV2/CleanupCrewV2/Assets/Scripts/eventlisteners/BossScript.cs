using UnityEngine;
using System.Collections;

public class BossScript : BaseGamestate {

    public override void StartState()
    {
        GameManager.instance.SetState(GameManager.gamestate.BreakoutIntermission);
    }

    public override void EndState()
    {

    }
}
