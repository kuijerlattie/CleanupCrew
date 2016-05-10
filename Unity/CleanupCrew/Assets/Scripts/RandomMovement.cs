using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class RandomMovement : MonoBehaviour {

    float force = 300f;
    Vector3 direction;
    float Speed = 5;
    Rigidbody _rigid;
	// Use this for initialization
	void Start () {
        //direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        direction = -Vector3.forward;
        _rigid = GetComponent<Rigidbody>();
        ApplyForce();

    }
	
	// Update is called once per frame
	void Update () {
        _rigid.velocity = _rigid.velocity.normalized * Speed;
    }

    void ApplyForce()
    {
        _rigid.AddForce(direction * force);
    }
}
