using UnityEngine;
using System.Collections;

public class MoleDefence : MonoBehaviour {

    public int side; //0 = left, 1 = right;

	// Use this for initialization
	void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Ball")
        {
            GetComponentInParent<MoleScript>().Block(side);
        }
    }
}
