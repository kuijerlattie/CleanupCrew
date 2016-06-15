using UnityEngine;
using System.Collections;

public class shieldScript : MonoBehaviour {

    void Update()
    {
        transform.localScale -= new Vector3(0, 0, 0.1f * Time.deltaTime); // make shield smaller overtime

        if(transform.localScale.z < Mathf.Epsilon)
        {
            Destroy(gameObject);
        }
    }

	void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag != "Ball")
        {
            Destroy(col.collider.gameObject);
        }

        else
            Destroy(gameObject);
    }
}
