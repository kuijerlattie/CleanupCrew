using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class RandomMovement : MonoBehaviour {

    Vector3 direction;
    Rigidbody _rigid;
	// Use this for initialization
	void Start () {
        //direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        direction = -Vector3.forward;
        _rigid = GetComponent<Rigidbody>();
        _rigid.velocity = direction;

    }
	



}
