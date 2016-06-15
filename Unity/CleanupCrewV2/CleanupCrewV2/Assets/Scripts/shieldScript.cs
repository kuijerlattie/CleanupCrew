using UnityEngine;
using System.Collections;

public class shieldScript : MonoBehaviour {

	void OnCollionEnter(Collision col)
    {
        if (col.collider.tag != "Ball")
        {
            Destroy(col.collider.gameObject);
        }
    }
}
