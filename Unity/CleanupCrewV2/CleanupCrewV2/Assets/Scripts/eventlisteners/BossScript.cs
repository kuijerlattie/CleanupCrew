using UnityEngine;
using System.Collections;


//script for whatever you want to do during the bossfight, does not include boss mechanics itself
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
