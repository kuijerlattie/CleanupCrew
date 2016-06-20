using UnityEngine;
using System.Collections;

public class MoleTail : MonoBehaviour {
    public int hitregion;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GetComponentInParent<MoleScript>().Hit(1, hitregion);
        }
    }
}
