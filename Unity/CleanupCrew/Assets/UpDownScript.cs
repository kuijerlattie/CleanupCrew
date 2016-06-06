using UnityEngine;
using System.Collections;

public class UpDownScript : MonoBehaviour {

    public float secondsUp;
    public float secondsDown;
    public float startAfterXSeconds = 1;
    private float currentTimer = 0;
    bool IsDown = true;
    float lastTriggered = 0;
	// Use this for initialization
	void Start () {
        currentTimer = startAfterXSeconds;
        EventManager.StartListening("RODHIT", DoTrigger);
	}
	
    void DoTrigger(GameObject g, float f)
    {
        if (lastTriggered > 0) return;
        SpawnSpheres.SpawnSphere(g.transform.position);
        GoDown();
        lastTriggered = 0.1f;
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.layer != LayerMask.NameToLayer("Balls")) return;
        EventManager.TriggerEvent("RODHIT", gameObject);
    }


    void GoUp()
    {
        currentTimer = secondsUp;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        IsDown = false;
        return;
    }
    void GoDown()
    {
        currentTimer = secondsDown;
        transform.position = new Vector3(transform.position.x, -100, transform.position.z);
        IsDown = true;
        return;
    }
	// Update is called once per frame
	void Update () {
        currentTimer -= Time.deltaTime;
        lastTriggered -= Time.deltaTime;
        
	    if(currentTimer <= 0)
        {
            if (IsDown)
            {

                GoUp();
                return;
            }

            if (!IsDown)
            {
                GoDown();
                return;
             
            }
        }
	}


}
