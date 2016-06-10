using UnityEngine;
using System.Collections;


/// <summary>
/// makes sure the speed of gameobject stays constant 
/// optional: keeps constant direction
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class FixedSpeed : MonoBehaviour {

    Rigidbody _rigid;
    public float targetSpeed = 10;
    public Vector3 fixedDirection = Vector3.zero;   //currently doesnt work with blobs hitting rods
	// Use this for initialization
	void Start () {
        _rigid = GetComponent<Rigidbody>();
	}
	

    public void ResetSpeed()
    {
        if (_rigid.velocity.magnitude != targetSpeed)
        {
            if (fixedDirection != Vector3.zero) _rigid.velocity = fixedDirection;
            _rigid.velocity = _rigid.velocity.normalized * targetSpeed;
        }
            
    }


    void OnCollisionEnter(Collision c)
    {
        ResetSpeed();
    }

}
