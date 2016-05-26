using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class ForceNotTrigger : MonoBehaviour {

    Collider col;

    void Start()
    {
        col = GetComponent<Collider>();
    }

	// Update is called once per frame
	void Update () {
        col.isTrigger = false;
	}
}
