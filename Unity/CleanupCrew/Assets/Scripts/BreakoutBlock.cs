using UnityEngine;
using System.Collections;

public class BreakoutBlock : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter()
    {
        //give points


        //destroy
        GameObject.Destroy(gameObject);
    }
}
