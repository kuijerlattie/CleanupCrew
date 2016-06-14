using UnityEngine;
using System.Collections;

public class BossScript : BaseGamestate {

    float i = 0;
    public override void StartState()
    {
        EventManager.StartListening("BossDied", BossDied);
    }

    public override void EndState()
    {
        EventManager.StopListening("BossDied", BossDied);
    }

    void Update()
    {
        
    }

    public void BossDied(GameObject g, float f)
    {
        EventManager.StopListening("BossDied", BossDied);
        GameManager.instance.SetState(GameManager.gamestate.BreakoutIntermission);
    }
}
