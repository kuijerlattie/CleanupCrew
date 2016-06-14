using UnityEngine;
using System.Collections;

public abstract class PwrupBase : MonoBehaviour {

    public abstract void startPwrup();

    public abstract void stopPwrup();

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Paddle")
        {
            startPwrup();
            GameObject.Destroy(gameObject);
        }

        if(col.collider.gameObject == GameObject.Find("bottom wall"))
        {
            GameObject.Destroy(gameObject);
        }

    }
}
