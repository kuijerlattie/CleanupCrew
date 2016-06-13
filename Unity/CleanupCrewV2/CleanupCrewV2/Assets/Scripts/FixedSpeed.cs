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
    bool slowFixDirection = false;
    // Use this for initialization
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        ResetSpeed();
    }

    public void ResetSpeed()
    {
        if (gameObject.tag == "Ball")
        {
            if (_rigid.velocity.magnitude < targetSpeed)
            {
                _rigid.velocity = _rigid.velocity.normalized * targetSpeed;
            }
            if (_rigid.velocity.magnitude > targetSpeed)
            {
                _rigid.velocity = _rigid.velocity.normalized * (_rigid.velocity.magnitude * 0.95f);
            }
        }
        else
        {
            if (_rigid.velocity.magnitude != targetSpeed)
            {
                //if (fixedDirection != Vector3.zero) _rigid.velocity = fixedDirection;
                _rigid.velocity = _rigid.velocity.normalized * targetSpeed;
            }
        }
    }

    public void SlowResetDirection()
    {
        if (fixedDirection == Vector3.zero) return;
        slowFixDirection = true;
    }

    public void InstantResetDirection()
    {
        if (fixedDirection == Vector3.zero) return;
        _rigid.velocity = fixedDirection;
        ResetSpeed();
    }


    void OnCollisionEnter(Collision c)
    {
        ResetSpeed();
        if (c.collider.gameObject.GetComponent<RodScript>())
        {
            SlowResetDirection();
        }
        else InstantResetDirection();
    }

    void Update()
    {
        if(slowFixDirection)
        {
            if(_rigid.velocity == fixedDirection)
                slowFixDirection = false;
            else
            {
                if (_rigid.velocity.z > _rigid.velocity.x) _rigid.velocity += new Vector3(1, 0, 0);
                _rigid.velocity = Vector3.MoveTowards(_rigid.velocity, fixedDirection, Time.deltaTime * 50);
                ResetSpeed();
            }
        }
        ResetSpeed();
    }

}
