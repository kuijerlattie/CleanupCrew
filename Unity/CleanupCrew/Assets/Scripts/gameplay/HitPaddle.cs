using UnityEngine;
using System.Collections;

public class HitPaddle : MonoBehaviour {

    [HideInInspector]
    public bool HittedPaddle = false;
	// Use this for initialization
	
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.gameObject.tag == "paddle")
        {
            if (GetComponent<Renderer>() == null) return;
            GetComponent<Renderer>().material.color = Color.green;
            HittedPaddle = true;
        }
    }
}
