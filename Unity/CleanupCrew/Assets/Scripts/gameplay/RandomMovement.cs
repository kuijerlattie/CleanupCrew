using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class RandomMovement : MonoBehaviour {

    Vector3 _direction;
    Rigidbody _rigid;
    bool _overrideDirection = false;
	// Use this for initialization
	void Start () {
        if (_overrideDirection) return;
        _direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        _rigid = GetComponent<Rigidbody>();
        _rigid.velocity = _direction;

    }

    public void OverrideDirection(Vector3 direction)
    {
        _overrideDirection = true;
        _rigid = GetComponent<Rigidbody>();
        _rigid.velocity = direction;
    }
	



}
