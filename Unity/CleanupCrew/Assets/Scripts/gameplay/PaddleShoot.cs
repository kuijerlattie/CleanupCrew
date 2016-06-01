using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// makes blobs/balls stick to paddle, 
/// SHOOT method to shoot them, TODO replace controls to shoot to a on screen button
/// </summary>
public class PaddleShoot : MonoBehaviour
{
   
    private List<GameObject> spheres = new List<GameObject>();
    public bool OverlapOnPaddle = true; //if balls etc dont collide with eachother this is always true

    void OnCollisionEnter(Collision col)
    {
        GameObject hitBall = col.gameObject;

        if (hitBall.layer != LayerMask.NameToLayer("Balls") && hitBall.layer != LayerMask.NameToLayer("Blobs")) return;

        Rigidbody hitBallRigid = hitBall.GetComponent<Rigidbody>();
        if(Vector3.Dot(hitBallRigid.velocity, gameObject.transform.forward) < 0)
        {
            return; //bounces back any blobs etc that are behind the paddle, so they dont get stuck
        }


        //allow them to move with the paddle until being shot away
        hitBallRigid.velocity = Vector3.zero;
        hitBall.transform.parent = gameObject.transform.parent;
        if(OverlapOnPaddle) hitBall.GetComponent<Collider>().isTrigger = true;
        spheres.Add(hitBall);
    }

    /// <summary>
    /// shoots all currently attached objects to the paddle towards the center, currently at 100% accuracy
    /// </summary>
    public void Shoot()
    {
        foreach(GameObject g in spheres)
        {
            if (g == null) continue;
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
