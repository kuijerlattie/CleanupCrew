using UnityEngine;
using System.Collections;

public class StickyPaddle : MonoBehaviour {

    GameObject levelcenter;

	// Use this for initialization
	void Start () {
        levelcenter = FindObjectOfType<PaddleRotationScript>().gameObject;
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "paddle")
        {
           
            
        }
        
    }
}
