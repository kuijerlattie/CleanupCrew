using UnityEngine;
using System.Collections;

public class BallBlobCollision : MonoBehaviour {

	void OnCollisionEnter(Collision c)
    {
        if(c.collider.gameObject.layer == LayerMask.NameToLayer("Blobs"))
            GameObject.Destroy(c.collider.gameObject);
    }
}
