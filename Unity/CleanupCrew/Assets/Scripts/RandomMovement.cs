using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Explosion), typeof(SphereCollider))]
public class RandomMovement : MonoBehaviour {

    float force = 300f;
    Vector3 direction;
    Rigidbody _rigid;
	// Use this for initialization
	void Start () {
        direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        _rigid = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        if (_rigid.velocity.magnitude < 3.1f)
            Debug.Log(_rigid.velocity.magnitude);
        if(_rigid.velocity.magnitude < 3.1f)
        {
            ApplyForce();
        }
    }

    void ApplyForce()
    {
        _rigid.AddForce(direction * force);
    }
}
