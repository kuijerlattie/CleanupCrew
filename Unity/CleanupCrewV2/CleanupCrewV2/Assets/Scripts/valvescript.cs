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
            Debug.Log("got hit m8");
            EventManager.TriggerEvent("ValveHit", col.gameObject, valveId);
        }
    }
}
