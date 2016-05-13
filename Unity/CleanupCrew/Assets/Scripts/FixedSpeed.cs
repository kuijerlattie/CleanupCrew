using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FixedSpeed : MonoBehaviour {

    private Rigidbody _rigid;
    [HideInInspector]
    public float Speed = 5.0f;

	// Use this for initialization
	void Start () {

        _rigid = GetComponent<Rigidbody>();
        _rigid.constraints = RigidbodyConstraints.FreezeRotation;
        _rigid.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
        _rigid.velocity = _rigid.velocity.normalized * Speed;
    }
}
