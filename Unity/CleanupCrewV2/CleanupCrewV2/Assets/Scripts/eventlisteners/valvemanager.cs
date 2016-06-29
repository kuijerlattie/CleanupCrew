using UnityEngine;
using System.Collections;

public class valvemanager : MonoBehaviour {
    valvescript[] valves;

    void Start()
    {
        valves = GameObject.FindObjectsOfType<valvescript>(); //valve, plz fix
        EventManager.StartListening("ValveHit", OnValveHit);
    }

    void OnDisable()
    {
        EventManager.StopListening("ValveHit", OnValveHit);
    }

    void OnValveHit(GameObject g, float f)
    {
        if (GameManager.instance.CurrentGamestate == GameManager.gamestate.Boss)
        {
            resetAllValves();
            return;
        }

        if (CheckAllValves())
        {
            resetAllValves();
            GameManager.instance.SetState(GameManager.gamestate.BossIntermission);
        }
    }

    bool CheckAllValves()
    {
        foreach (valvescript v in valves)
        {
            if (!v.activated) return false;
        }
        return true;
    }

    void resetAllValves()
    {
        foreach (valvescript v in valves)
            v.activated = false;
    }
}
