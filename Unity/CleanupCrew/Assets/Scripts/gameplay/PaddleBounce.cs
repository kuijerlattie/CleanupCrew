using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class PaddleBounce : MonoBehaviour {

	void OnCollisionEnter(Collision col)
    {
        GameObject hitBall = col.gameObject;

        if (hitBall.layer != LayerMask.NameToLayer("Balls")) return;

        Rigidbody hitBallRigid = hitBall.GetComponent<Rigidbody>();
        

        Vector3 outPutDirection = Vector3.Dot(gameObject.transform.forward, gameObject.transform.position - hitBall.transform.position) < 0 ?
                                  gameObject.transform.forward : -gameObject.transform.forward;

        //based on how far to the edge of the paddle the ball is hit, it gets velocity to the side
        Vector3 hitPoint = col.contacts[0].point;

        float paddleLength = gameObject.transform.localScale.x;
        outPutDirection += (hitPoint - gameObject.transform.position) / paddleLength * 2.0f;

        hitBallRigid.velocity = outPutDirection.normalized;


    }
}
