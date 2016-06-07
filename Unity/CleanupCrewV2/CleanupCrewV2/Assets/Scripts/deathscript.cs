using UnityEngine;
using System.Collections;

public class deathscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ball")
        {
            EventManager.TriggerEvent("BallBottomDeath", col.gameObject, 0f);
        }

        if (col.gameObject.tag == "blob")
        {
            EventManager.TriggerEvent("BlobBottomDeath", col.gameObject, 0f);
        }
    }
}
