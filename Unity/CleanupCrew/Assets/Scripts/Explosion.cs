using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(RandomMovement),typeof(SphereCollider))]
public class Explosion : MonoBehaviour {

    //list of all spheres that exploded into one object, NEEDS TO BE CLEARED WHEN STARTING NEW LEVEL
    public static List<Explosion> explodedSpheres = new List<Explosion>();  

    const float MAXSIZE = 7.5f;
    const float GROWTHRATE = 7.5f;  // scale * X   per second
	// Use this for initialization
	void Start () {
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<RandomMovement>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        explodedSpheres.Add(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x < MAXSIZE)
            transform.localScale = new Vector3(transform.localScale.x * 1+ ( GROWTHRATE * Time.deltaTime), transform.localScale.y, transform.localScale.z * 1 +( GROWTHRATE * Time.deltaTime));
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {
            Explosion collidedExplosion = col.gameObject.GetComponent<Explosion>();
            collidedExplosion.enabled = true;
        }
    }


}
