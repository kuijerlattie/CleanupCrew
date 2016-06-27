using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]    //needs collider to work out the maximum Y-value in 'Down-state' 
public class RodScript : MonoBehaviour {


    private float currentState = RodState.Up;

    private Vector3 targetPos = Vector3.up;
    public float RodSpeed = 2;  //how fast it moves up and down

    public float SwitchAfterSeconds = 5;
    private float switchTimer;
    private float maxHeight = 9;

    public bool StartUp = true;
    public bool AllowedToSwitch = true; //alternative to disable this script

    public void EnableRod()
    {
        AllowedToSwitch = true;
        if (currentState == RodState.Down) SwitchState();
    }
    /// <summary>
    /// keeps the rod in the 'Down-position' until 'EnableRod' is called
    /// </summary>
    public void DisableRod()
    {
        if (currentState == RodState.Up) SwitchState();
        AllowedToSwitch = false;
    }

    public static void DisableAllRods()
    {
        RodScript[] rods = FindObjectsOfType<RodScript>();
        foreach (RodScript rod in rods)
        {
            rod.DisableRod();
        }
    }

    public static void EnableAllRods()
    {
        RodScript[] rods = FindObjectsOfType<RodScript>();
        foreach(RodScript rod in rods)
        {
            rod.EnableRod();
        }
    }


    public static Vector3 GetRodSpawnPoint(GameObject rod)
    {
        RodScript RS = rod.GetComponent<RodScript>();
        if (!RS) Debug.LogError("ERROR: '" + rod.name + "' does not have a 'RodScript' attached");
        return RS.SpawnPoint;
    }
    public Vector3 SpawnPoint { get {
            if (_SpawnPoint == Vector3.zero) SetSpawnPoint();
            return _SpawnPoint;
        } }
    private Vector3 _SpawnPoint = Vector3.zero;
    private void SetSpawnPoint()
    {
        _SpawnPoint = new Vector3(transform.position.x, BlobScript.GetRandomSpawnPos.y, transform.position.z - GetComponent<Collider>().bounds.size.z / 2.0f - 2);
    }



    public void SwitchState()
    {
        currentState = currentState == RodState.Up ? RodState.Down : RodState.Up;
        targetPos.y = (int)currentState;    //takes value from the up/down state to use as target heigth
        switchTimer = SwitchAfterSeconds;   //reset timer
        EventManager.TriggerEvent("RodMoved", gameObject, targetPos.y);
    }

	// Use this for initialization
	void Start () {
        Init();
        currentState = StartUp ? RodState.Up : RodState.Down;
        transform.position = new Vector3(transform.position.x, currentState, transform.position.z);
        targetPos = transform.position;
    }

    void Init()
    {
        switchTimer = SwitchAfterSeconds;
        targetPos = transform.position;
        RodState.Down = -(GetComponent<Collider>().bounds.size.y / 2.0f + float.Epsilon + maxHeight);   

    }
	
	// Update is called once per frame
	void Update () {
        if (targetPos == Vector3.up) targetPos = transform.position;    //for safety, targetPos SHOULD be set in 'Init'


        //timer until 'SwitchState' is executed
        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
            if (switchTimer < 1) EventManager.TriggerEvent("RodAboutToMove", gameObject, switchTimer);  //if x second left until switch, trigger event
        }
        else if(AllowedToSwitch) SwitchState(); //only switch when allowed

        //movement during switching of up/down
	    if((targetPos - transform.position).magnitude > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * RodSpeed);
        }
	}

    public static rodtype RandomType
    {
        get
        {
            rodtype[] rodtypeValues = (rodtype[])Enum.GetValues(typeof(rodtype));
            return rodtypeValues[UnityEngine.Random.Range(0, rodtypeValues.GetLength(0))];
        }
    }
 
    public enum rodtype
    {
        water,
        space,
        underground
    }

    public rodtype rodType;



    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Ball")
        {
            if (GameManager.instance.CurrentGamestate != GameManager.gamestate.BossIntermission)
            {
                //make ball bounce off 4x the speed
                col.gameObject.GetComponent<Rigidbody>().velocity *= 4;
                col.gameObject.GetComponent<FixedSpeed>().SlowResetSpeed(); //to make sure the speed drops off quickly after the speedup
                EventManager.TriggerEvent("BallHitRod", this.gameObject, (float)rodType);
            }
        }

        if (col.gameObject.tag == "Blob")
        {
            EventManager.TriggerEvent("BlobHitRod", col.gameObject, (float)rodType);
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Blob")
        {
            EventManager.TriggerEvent("BlobCrushed", col.gameObject);
            //Destroy(col.gameObject);
        }
    }

    //custom 'enum' of which its values can be changed during runtime
    //the values of the states are used for the Y-value of the rod
    //EDIT: this might not work with static if the different rods have different heights(which they should NOT)
    private static class RodState
    {
        static public float Up = 0; //is overwritten later
        static public float Down = -4;
    }


}
