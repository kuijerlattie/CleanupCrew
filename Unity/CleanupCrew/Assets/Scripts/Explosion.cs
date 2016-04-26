using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RandomMovement),typeof(SphereCollider))]
public class Explosion : MonoBehaviour {

    const float MAXSIZE = 7.5f;
    const float GROWTHRATE = 1.05f;
	// Use this for initialization
	void Start () {
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<RandomMovement>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x < MAXSIZE)
            transform.localScale = new Vector3(transform.localScale.x * GROWTHRATE, transform.localScale.y, transform.localScale.z * GROWTHRATE);
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Balls"))
            col.gameObject.GetComponent<Explosion>().enabled = true;
    }


}
