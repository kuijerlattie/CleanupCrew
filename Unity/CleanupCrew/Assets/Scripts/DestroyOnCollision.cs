using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class DestroyOnCollision : MonoBehaviour
{
    //currently this whole script doesnt work
    void OnTriggerEnter(Collider col)
    {
        return;
        if(col.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {
            if (!col.gameObject.GetComponent<HitPaddle>().HittedPaddle) return;
            GameObject.Destroy(col.gameObject);
            GameObject.Destroy(gameObject);
            
        }
    }
}
