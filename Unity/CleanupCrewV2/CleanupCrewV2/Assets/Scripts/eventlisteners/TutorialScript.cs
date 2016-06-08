using UnityEngine;
using System.Collections;

public class TutorialScript : BaseGamestate {

    public override void StartState()
    {
        GameManager.instance.SetState(GameManager.gamestate.BreakoutIntermission);
    }

    public override void EndState()
    {

    }
}
