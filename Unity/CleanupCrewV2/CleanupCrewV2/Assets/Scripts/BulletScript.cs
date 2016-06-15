using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

    void Update()
    {
        GetComponent<Rigidbody>().velocity = GetComponent<FixedSpeed>().fixedDirection;
    }

	void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Blob")
        {
            Destroy(col.collider.gameObject);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);

    }
}
