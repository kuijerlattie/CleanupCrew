using UnityEngine;
using System.Collections;

//           volvomanager
public class valvemanager : MonoBehaviour {
    valvescript[] valves;

    Material outlineMat = null;
    void Start()
    {
        valves = GameObject.FindObjectsOfType<valvescript>(); //volvo, plz fix
        EventManager.StartListening("ValveHit", OnValveHit);
        outlineMat = new Material(Shader.Find("Outlined/Silhouette Only"));


    }

    public void AddValveOutline()
    {
        foreach (valvescript valve in valves)
        {
            valve.gameObject.GetComponent<Renderer>().materials = new Material[] { valve.gameObject.GetComponent<Renderer>().materials[0], outlineMat };
        }
    }


    public void RemoveValveOutline()
    {
        foreach (valvescript valve in valves)
        {
            valve.gameObject.GetComponent<Renderer>().materials = new Material[] { valve.gameObject.GetComponent<Renderer>().materials[0]};
        }
    }

    public static valvemanager instance { get { return GameObject.FindObjectOfType<valvemanager>(); } }

    public void SetValveOutlineColor(Color c)
    {
        foreach (valvescript valve in valves)
        {
            if(valve.gameObject.GetComponent<Renderer>().materials.GetLength(0) > 1)
                valve.gameObject.GetComponent<Renderer>().materials[1].SetColor("_OutlineColor", c);
        }
       
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
        {
            v.activated = false;
            SetValveOutlineColor(Color.red);
        }
    }
}
