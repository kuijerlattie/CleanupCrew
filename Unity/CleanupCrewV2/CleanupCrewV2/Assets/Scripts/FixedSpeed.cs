﻿using UnityEngine;
using System.Collections;


/// <summary>
/// makes sure the speed of gameobject stays constant 
/// optional: keeps constant direction
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class FixedSpeed : MonoBehaviour {

    Rigidbody _rigid;
    public float targetSpeed = 10;
    public float maxSpeed = 50;
    public Vector3 fixedDirection = Vector3.zero;   //currently doesnt work with blobs hitting rods
    bool slowFixDirection = false;
    bool slowResetSpeed = false;
    // Use this for initialization
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        ResetSpeed();
    }

    public void SlowResetSpeed()
    {
        slowResetSpeed = true;
    }

    /// <summary>
    /// to make sure the ball won't get stuck bouncing left to right, and not going up or down
    /// </summary>
    private void AdjustDirection()
    {
        if (gameObject.tag != "Ball") return;
        if(Mathf.Abs(_rigid.velocity.x) > Mathf.Abs(_rigid.velocity.z))
        {
            _rigid.velocity = new Vector3(_rigid.velocity.x / 1.5f, 0, (_rigid.velocity.z - 0.3f) * 1.5f);

        }
        if(_rigid.velocity.normalized == -Vector3.forward)  // to make sure it doesnt get behind a rod and keeps going against the top wall
        {
            _rigid.velocity += new Vector3(1f, 0, 0);
        }
    }

    public void ResetSpeed()
    {
        if (_rigid == null) _rigid = GetComponent<Rigidbody>();
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
        if (c.gameObject.tag == "Wall")
            AdjustDirection();
        ResetSpeed();
        if (c.collider.gameObject.GetComponent<RodScript>())
        {
            SlowResetDirection();
            if (gameObject.tag == "Blob")
            {
                StopCoroutine("StartBehaviour");
                gameObject.GetComponent<BlobScript>().StopBehaviour(0.1f);
            }
        }
        else InstantResetDirection();

        
    }

    void Update()
    {
        if(slowResetSpeed)
        {
            if (_rigid.velocity.magnitude == targetSpeed)
                slowResetSpeed = false;
            else
            {
                _rigid.velocity = Vector3.MoveTowards(_rigid.velocity, _rigid.velocity.normalized * targetSpeed, Time.deltaTime * 300);
            }
        }


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
        if(!slowResetSpeed) ResetSpeed();
        if (_rigid.velocity.magnitude > maxSpeed) _rigid.velocity = _rigid.velocity.normalized * maxSpeed;
    }

}
