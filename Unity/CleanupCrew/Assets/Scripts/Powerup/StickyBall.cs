using UnityEngine;
using System.Collections.Generic;

public class StickyBall : MonoBehaviour {

    public bool Sticky = false;
    GameObject paddle;
    

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	if(Sticky)
        {
            gameObject.transform.position = paddle.transform.position + paddle.transform.forward;
            
        }

    if(Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.GetComponent<Rigidbody>().velocity = paddle.transform.forward;
            Sticky = false;
            Destroy(this);
        }
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.tag == "paddle")
        {
            Sticky = true;
            paddle = other.gameObject;

            GameObject[] blobs;
            blobs = GameObject.FindGameObjectsWithTag("Blob");

            foreach(GameObject b in blobs)
            {
                if(b.GetComponent<StickyBall>().Sticky == false)
                {
                    b.gameObject.GetComponent<StickyBall>().DestroySticky();
                }
            }
        }

        
    }

    public void DestroySticky()
    {
        Destroy(this);
    }
}
