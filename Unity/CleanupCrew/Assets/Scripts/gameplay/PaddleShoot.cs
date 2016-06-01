using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// makes blobs/balls stick to paddle, 
/// SHOOT method to shoot them, TODO replace controls to shoot to a on screen button
/// </summary>
public class PaddleShoot : MonoBehaviour
{
   
    private List<GameObject> spheres = new List<GameObject>();
    public bool OverlapOnPaddle = true;

    void OnCollisionEnter(Collision col)
    {
        GameObject hitBall = col.gameObject;

        if (hitBall.layer != LayerMask.NameToLayer("Balls") && hitBall.layer != LayerMask.NameToLayer("Blobs")) return;

        Rigidbody hitBallRigid = hitBall.GetComponent<Rigidbody>();
        hitBallRigid.velocity = Vector3.zero;
        hitBall.transform.parent = gameObject.transform.parent;
        if(OverlapOnPaddle) hitBall.GetComponent<Collider>().isTrigger = true;
        spheres.Add(hitBall);
    }

    public void Shoot()
    {
        foreach(GameObject g in spheres)
        {
            g.GetComponent<Rigidbody>().velocity = -g.transform.position;
            g.transform.parent = null;
            if (OverlapOnPaddle) g.GetComponent<Collider>().isTrigger = false;
        }
        spheres.Clear();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) Shoot();   //TODO on screen button 
    }

}
