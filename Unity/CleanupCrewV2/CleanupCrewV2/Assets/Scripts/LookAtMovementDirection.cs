using UnityEngine;
using System.Collections;

public class LookAtMovementDirection : MonoBehaviour {
	// Update is called once per frame
	void Update () {
        transform.LookAt(transform.position + GetComponent<Rigidbody>().velocity);
	}
}
