using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// makes blobs/balls stick to paddle, 
/// SHOOT method to shoot them, TODO replace controls to shoot to a on screen button
/// </summary>
public class PaddleShoot : MonoBehaviour
{

    private int maxObjects = GameSettings.MaxPaddleObjectsS;
    private List<GameObject> spheres = new List<GameObject>();
    private bool OverlapOnPaddle = true; //if balls etc dont collide with eachother this is always true


    void OnCollisionEnter(Collision col)
    {
        GameObject hitBall = col.gameObject;

        if (hitBall.layer != LayerMask.NameToLayer("Balls") && hitBall.layer != LayerMask.NameToLayer("Blobs")) return;

        Rigidbody hitBallRigid = hitBall.GetComponent<Rigidbody>();

        //TODO make sure blobs dont get stuck on the side of the paddle
        if(Vector3.Dot(hitBallRigid.velocity.normalized, gameObject.transform.forward) < 0 || spheres.Count >= maxObjects)
        {
            return; //bounces back any blobs etc that are behind the paddle, so they dont get stuck
        }


        //allow them to move with the paddle until being shot away
        hitBallRigid.velocity = Vector3.zero;
        hitBall.transform.parent = gameObject.transform.parent;
        if(OverlapOnPaddle) hitBall.GetComponent<Collider>().isTrigger = true;
        MeshRenderer[] renderers = hitBall.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer r in renderers)
        {
            r.enabled = false;
        }
           
        spheres.Add(hitBall);
        UpdateColor();

    }

    /// <summary>
    /// only for prototype
    /// </summary>
    private void UpdateColor()
    {
        Renderer r = GetComponent<Renderer>();
        if (r == null) return;
        switch(spheres.Count)
        {
            case 0:
                r.material.color = Color.white;
                break;
            case 1:
                r.material.color = Color.green;
                break;
            case 2:
                r.material.color = new Color(255, 145, 0);  //orange
                break;
            case 3:
                r.material.color = Color.red;
                break;
        }
    }

    /// <summary>
    /// shoots all currently attached objects to the paddle towards the center, currently at 100% accuracy
    /// </summary>
    public void Shoot()
    {
        if (spheres.Count <= 0) return; //nothing to shoot
        GameObject g = spheres[0];
        if (g == null) return;  //nothing to shoot
        g.GetComponent<Rigidbody>().velocity = gameObject.transform.forward;
        g.transform.position = gameObject.transform.position + gameObject.transform.forward * 3;
        MeshRenderer[] renderers = g.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers)
        {
            r.enabled = true;
        }
        g.transform.parent = null;
        if (OverlapOnPaddle) g.GetComponent<Collider>().isTrigger = false;
        spheres.Remove(g);
        UpdateColor();
        /*
        foreach(GameObject g in spheres)    //all at once
        {
            if (g == null) continue;
            g.GetComponent<Rigidbody>().velocity = -g.transform.position;
            g.transform.parent = null;
            g.transform.position += gameObject.transform.forward;
            if (OverlapOnPaddle) g.GetComponent<Collider>().isTrigger = false;
        }
        spheres.Clear();
        */

    }

    void Update()
    {
        maxObjects = GameSettings.MaxPaddleObjectsS;
        if (Input.GetMouseButtonDown(1)) Shoot();   
    }

}
