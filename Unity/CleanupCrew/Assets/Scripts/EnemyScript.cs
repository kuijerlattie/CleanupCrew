using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EnemyScript : MonoBehaviour {

    float health = 100; //percent
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision col)
    {
        
        if(col.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {

        }

        if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {

        }
    }
}
