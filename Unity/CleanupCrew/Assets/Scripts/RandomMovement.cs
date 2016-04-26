using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour {

    float force = 100f;
    Vector3 direction;
	// Use this for initialization
	void Start () {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        GetComponent<Rigidbody>().AddForce(direction * force);

    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
