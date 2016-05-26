using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class DestroyOnCollision : MonoBehaviour
{
    //currently this whole script doesnt work
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
