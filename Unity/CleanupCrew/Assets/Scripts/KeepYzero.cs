using UnityEngine;
using System.Collections;

public class KeepYzero : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //transform.position = new Vector3(transform.position.x, 0, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.y != 0)
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);    //why is this needed? why does it not want to go to 0 otherwise?.... unity pls
    }
}
