using UnityEngine;
using System.Collections;

public class PaddlePhysics : MonoBehaviour {

    float maxangle = 90;

    void OnCollisionEnter(Collision col)
    {
        GameObject hitBall = col.gameObject;

        if (hitBall.tag == "Ball")
        {

            Rigidbody hitBallRigid = hitBall.GetComponent<Rigidbody>();


            Vector3 outPutDirection = Vector3.Dot(gameObject.transform.forward, gameObject.transform.position - hitBall.transform.position) < 0 ?
                                      gameObject.transform.forward : -gameObject.transform.forward;

            //based on how far to the edge of the paddle the ball is hit, it gets velocity to the side
            Vector3 hitPoint = col.contacts[0].point;
            Vector3 localhitpoint = transform.InverseTransformPoint(hitPoint);
            float desiredangle = localhitpoint.x * maxangle;

            float paddleLength = gameObject.transform.localScale.x;
            outPutDirection = Quaternion.AngleAxis(desiredangle, Vector3.up) * outPutDirection;
            outPutDirection.y = 0;

            hitBallRigid.velocity = outPutDirection.normalized;

            EventManager.TriggerEvent("BallHitPaddle", col.gameObject);
            hitBall.GetComponent<FixedSpeed>().ResetSpeed();
        }

    }
}
