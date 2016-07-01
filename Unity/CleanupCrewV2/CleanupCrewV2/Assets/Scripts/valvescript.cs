using UnityEngine;
using System.Collections;

public class valvescript : MonoBehaviour {

    // Use this for initialization
    public bool activated = false;
    public int valveId;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ball")
        {
            activated = true;
            SetOutlineColor(Color.green);
            EventManager.TriggerEvent("ValveHit", col.gameObject, valveId);
        }
    }

    public void SetOutlineColor(Color c)
    {
        //if(gameObject.GetComponent<Renderer>().materials.GetLength(0) < 2) return;

        gameObject.GetComponent<Renderer>().materials[1].SetColor("_OutlineColor", c);

    }
}
