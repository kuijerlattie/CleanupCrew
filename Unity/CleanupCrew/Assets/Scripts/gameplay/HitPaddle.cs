using UnityEngine;
using System.Collections;

public class HitPaddle : MonoBehaviour {

    public bool HittedPaddle { get; private set; }
	// Use this for initialization
	
    void OnCollisionEnter(Collision col)
    {
        if(col.collider.gameObject.tag == "paddle")
        {
            GetComponent<Renderer>().material.color = Color.green;
            HittedPaddle = true;
        }
    }
}
