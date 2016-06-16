using UnityEngine;
using System.Collections;

/// <summary>
/// currently empty just for collision checks etc
/// </summary>
/// 

[RequireComponent(typeof(Rigidbody))]
public class BallScript : MonoBehaviour {

    public float RotationSpeed = 10;    //is in inspector
    Rigidbody _rigid;
    private Vector3 RotationVec = Vector3.zero;

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        RotationVec = new Vector3(RotationSpeed, 0, 0);
    }

    void OnCollisionEnter(Collision c)
    {
        transform.LookAt(_rigid.velocity);
    }

    void Update()
    {
        transform.Rotate(RotationVec);
    }
}
