using UnityEngine;
using System.Collections;

/// <summary>
/// currently empty just for collision checks etc
/// </summary>
/// 

[RequireComponent(typeof(Rigidbody))]
public class BallScript : MonoBehaviour {

    float RotationSpeed = 2000;    //is in inspector
    Rigidbody _rigid;
    private Vector3 RotationVec = Vector3.zero;
    private PaddleControls PC;

    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
        RotationVec = new Vector3(RotationSpeed, 0, 0);
        PC = FindObjectOfType<PaddleControls>();
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Wall")
        { 
            EventManager.TriggerEvent("BallHitWall", gameObject);
        }
        transform.LookAt(transform.position + _rigid.velocity);
    }

    void Update()
    {
        if(PC.currentState == PaddleControls.PaddleState.Playing)
            transform.Rotate(RotationVec * Time.deltaTime);
    }
}
