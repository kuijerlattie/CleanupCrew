using UnityEngine;
using System.Collections;

public class rotationtest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   if(Input.GetKey(KeyCode.A))
        {
            gameObject.transform.RotateAround(Vector3.up, 0.1f);
        }
	}
}
