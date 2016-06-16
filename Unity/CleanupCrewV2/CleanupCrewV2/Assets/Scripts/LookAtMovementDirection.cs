using UnityEngine;
using System.Collections;

public class LookAtMovementDirection : MonoBehaviour {
    // Update is called once per frame
    Vector3 oldPos;

    void Start()
    {
        oldPos = transform.position;
    }

	void Update () {
        transform.LookAt(transform.position + (transform.position - oldPos));

        oldPos = transform.position;
	}
}
