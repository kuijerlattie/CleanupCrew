using UnityEngine;
using System.Collections;

public class BossIntermissionScript : BaseGamestate {

    public override void StartState()
    {
        GameManager.instance.SetState(GameManager.gamestate.Boss);
    }

    public override void EndState()
    {

    }
}
