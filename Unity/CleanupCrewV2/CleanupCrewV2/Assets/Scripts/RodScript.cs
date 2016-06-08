using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Collider))]    //needs collider to work out the maximum Y-value in 'Down-state' 
public class RodScript : MonoBehaviour {


    private float currentState = RodState.Down;

    private Vector3 targetPos = Vector3.up;
    public float RodSpeed = 2;  //how fast it moves up and down

    public float SwitchAfterSeconds = 5;
    private float switchTimer;
    private float maxHeight = 2;

    public static bool AllowedToSwitch = true; //alternative to disable this script

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
	}

    void Init()
    {
        switchTimer = SwitchAfterSeconds;
        targetPos = transform.position;
        RodState.Up = (GetComponent<Collider>().bounds.size.y / 2.0f + float.Epsilon + maxHeight);   

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
        if (col.gameObject.GetComponent<BallScript>() != null)
        {
            EventManager.TriggerEvent("BallHitRod", this.gameObject, (float)rodType);
        }

        if (col.gameObject.GetComponent<BlobScript>() != null)
        {
            EventManager.TriggerEvent("BlobHitRod", col.gameObject, (float)rodType);
        }
    }

    //custom 'enum' of which its values can be changed during runtime
    //the values of the states are used for the Y-value of the rod
    //EDIT: this might not work with static if the different rods have different heights(which they should NOT)
    private static class RodState
    {
        static public float Up = 5; //is overwritten later
        static public float Down = 0;
    }


}
