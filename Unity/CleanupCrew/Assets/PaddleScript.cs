using UnityEngine;
using System.Collections;

public class PaddleScript : MonoBehaviour {

    Transform trans;
    Vector3 mousepos;
	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	    //x
        mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        trans.position = new Vector3(mousepos.x, trans.position.y, trans.position.z);
	}
}
