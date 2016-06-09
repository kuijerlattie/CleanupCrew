using UnityEngine;
using System.Collections;

public class PaddlePhysics : MonoBehaviour {

	void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Paddle")
        {
            Vector3 hitpoint = col.contacts[0].point;
            Vector3 localhitpoint = col.transform.InverseTransformPoint(hitpoint);
            float pointalongx = localhitpoint.x;
            float DesiredAngle = pointalongx * 90;
            Vector3 currentvelocity = this.GetComponent<Rigidbody>().velocity;
            float currentspeed = currentvelocity.magnitude;
            currentvelocity.Normalize();
            //currentvelocity = Quaternion.AngleAxis()
            //TODO FINISH THIS SHIT
        }
    }
}
