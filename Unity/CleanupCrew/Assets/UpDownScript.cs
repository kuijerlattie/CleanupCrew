using UnityEngine;
using System.Collections;

/// <summary>
/// makes the rods go up/down at certain timer
/// </summary>
public class UpDownScript : MonoBehaviour {

    public float secondsUp;
    public float secondsDown;
    public float startAfterXSeconds = 1;
    private float currentTimer = 0;
    bool IsDown = true;
    float lastTriggered = 0;

    bool spawnInCenter = false; //if true spawns on position of this gameobject
	// Use this for initialization
	void Start () {
        currentTimer = startAfterXSeconds;
        EventManager.StartListening("RODHIT", DoTrigger);
	}

    /// <summary>
    /// to make sure that it cannot be hit twice in the same frame
    /// </summary>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator UnTrigger(float delay = 0 )
    {
        yield return new WaitForSeconds(delay);
        GetComponent<Collider>().isTrigger = false;
        yield return null;
    }
	
    void DoTrigger(GameObject g, float f)
    {
        if (lastTriggered > 0) return;
        StartCoroutine(SpawnSpheres.SpawnWithDelay(spawnInCenter? Vector3.zero: g.transform.position, true, 0.2f));
        GoDown();
        lastTriggered = 0.1f;
    }

    void OnCollisionEnter(Collision c)
    {
        
        if (c.collider.gameObject.layer != LayerMask.NameToLayer("Balls") || c.collider.gameObject.GetComponent<BallBlobCollision>() == null) return;
        // EventManager.TriggerEvent("RODHIT", gameObject);
        DoTrigger(gameObject, 0);
        GetComponent<Collider>().isTrigger = true;
        UnTrigger(10);
        Debug.Log(true);
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
