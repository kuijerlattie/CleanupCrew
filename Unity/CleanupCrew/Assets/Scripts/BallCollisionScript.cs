using UnityEngine;
using System.Collections;

public class BallCollisionScript : MonoBehaviour {
    
    public bool touched;
    
    GameObject ball;
    
	// Use this for initialization
	void Start () {
        touched = false;
        ball = GameObject.FindGameObjectWithTag("Ball");
	}
	
	// Update is called once per frame
	void Update () {
	    if(touched == true)
        {
            Physics.IgnoreLayerCollision(10, 9, true);
        }
	}

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "paddle")
        {
            touched = true;
            gameObject.layer = LayerMask.NameToLayer("WallIgnoring");
        }
    }
}
