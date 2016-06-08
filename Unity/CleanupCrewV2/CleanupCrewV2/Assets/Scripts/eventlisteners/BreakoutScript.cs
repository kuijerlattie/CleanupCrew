using UnityEngine;
using System.Collections;

public class BreakoutScript : BaseGamestate {

    public override void StartState()
    {

    }

    public override void EndState()
    {

    }

    void Update()
    {
        if (GameManager.instance.CurrentEarthPoints > 10)
        {
            GameManager.instance.SetState(GameManager.gamestate.BossIntermission);
        }
        if (GameManager.instance.CurrentWaterPoints > 10)
        {
            GameManager.instance.SetState(GameManager.gamestate.BossIntermission);
        }
        if (GameManager.instance.CurrentEarthPoints > 10)
        {
            GameManager.instance.SetState(GameManager.gamestate.BossIntermission);
        }
    }
}
