using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// makes blobs/balls stick to paddle, and enables shooting them towards the center
/// </summary>
public class PaddleShoot : MonoBehaviour
{
    doubleClick DC = new doubleClick();
    private int maxObjects = GameSettings.MaxPaddleObjectsS;
    private List<GameObject> spheres = new List<GameObject>();
    private bool OverlapOnPaddle = true; //if balls etc dont collide with eachother this is always true
    private int currentSpheres;
    public int sphereCount
    { get { return spheres.Count; } }

    [HideInInspector]
    public bool CanShoot = true;

    void Start()
    {
        if (GameObject.FindObjectsOfType<PaddleShoot>().GetLength(0) > 1) CanShoot = false;
        UpdateColor();
    }

    public void AddObject(GameObject g)
    {
        //allow them to move with the paddle until being shot away
        g.GetComponent<Rigidbody>().velocity = Vector3.zero;
        g.transform.parent = gameObject.transform.parent;
        if (OverlapOnPaddle) g.GetComponent<Collider>().isTrigger = true;
        MeshRenderer[] renderers = g.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers)
        {
            r.enabled = false;  //makes all renderes invisible
        }

        spheres.Add(g);
        UpdateColor();
    }

    void OnCollisionEnter(Collision col)
    {
        GameObject hitBall = col.gameObject;

        if (hitBall.layer != LayerMask.NameToLayer("Balls") && hitBall.layer != LayerMask.NameToLayer("Blobs")) return;

        if (hitBall.name.Contains("Powerup") || hitBall.name.Contains("powerup") || hitBall.name.Contains("up")) return;

        Rigidbody hitBallRigid = hitBall.GetComponent<Rigidbody>();

        //TODO make sure blobs dont get stuck on the side of the paddle
        if(Vector3.Dot(hitBallRigid.velocity.normalized, gameObject.transform.forward) < 0 || spheres.Count >= maxObjects)
        {
            return; //bounces back any blobs etc that are behind the paddle, so they dont get stuck
        }

        if (!CanShoot)
        {
            FindMainPaddle().AddObject(hitBall);
            return;
        }

        AddObject(hitBall);

    }

    /// <summary>
    /// changes color depending on how many blobs are currently inside the paddle
    /// </summary>
    private void UpdateColor()
    {
        Renderer r = GetComponent<Renderer>();
        if (r == null) return;
        
        switch (spheres.Count)
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
        if (!CanShoot) r.material.color = Color.black;
    }

    void CleanupList()
    {
        for (int i = spheres.Count-1; i > 0; i--)
        {
            if (spheres[i] == null) spheres.RemoveAt(i);
        }
    }

    public static PaddleShoot FindMainPaddle()
    {
        PaddleShoot[] PS = GameObject.FindObjectsOfType<PaddleShoot>();
        foreach(PaddleShoot ps in PS)
        {
            if (ps.CanShoot) return ps;
        }
        return GameObject.FindObjectOfType<PaddleShoot>();  //default... just a random one
    }

    /// <summary>
    /// shoots all currently attached objects to the paddle towards the center, currently at 100% accuracy
    /// </summary>
    public void Shoot()
    {
        if (!CanShoot) return;
        if (spheres.Count <= 0) return; //nothing to shoot
        GameObject g = spheres[0];
        if (g == null) return;  //nothing to shoot
        g.GetComponent<Rigidbody>().velocity = gameObject.transform.forward;
        g.transform.position = gameObject.transform.position + gameObject.transform.forward * 3;
        MeshRenderer[] renderers = g.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers)
        {
            r.enabled = true;   //makes all renderes visible again
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

    public void CleanPaddle()
    {
        for (int i = spheres.Count - 1; i > 0; i--)
        {
            GameObject.Destroy(spheres[i]);
        }
        spheres.Clear();
        UpdateColor();
    }

    /// <summary>
    /// use this on every phase switch to make sure nothign gets stuck in the paddle
    /// </summary>
    public static void CleanPaddlesS()
    {
        FindMainPaddle().CleanPaddle();
    }

    void OnDestroy()
    {
        CleanPaddle();

    }


    void Update()
    {
        CleanupList();
        maxObjects = GameSettings.MaxPaddleObjectsS;    //in update in case it is changed during gameplay.   TODO if for sure this doesnt change move to 'Start()'
                                                        // if (Input.GetMouseButtonDown(1)) Shoot();   
        DC.UpdateMouse();
       
    }

    public class doubleClick
    {
        bool one_click = false;
        bool timer_running;
        float timer_for_double_click;

        public void UpdateMouse()
        {
            //this is how long in seconds to allow for a double click
            float delay = 0.2f;
            if (Input.GetMouseButtonDown(0))
            {
                if (!one_click) // first click no previous clicks
                {
                    one_click = true;

                    timer_for_double_click = Time.time; // save the current time
                                                        // do one click things;
                }
                else
                {
                    one_click = false; // found a double click, now reset
                    GameObject.FindObjectOfType<PaddleShoot>().Shoot();
                    //do double click things
                }
            }
            if (one_click)
            {
                // if the time now is delay seconds more than when the first click started. 
                if ((Time.time - timer_for_double_click) > delay)
                {

                    //basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.

                    one_click = false;

                }
            }

        }
    }

}
