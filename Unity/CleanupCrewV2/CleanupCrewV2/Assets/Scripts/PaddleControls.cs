using UnityEngine;
using System.Collections;
using System;

public class PaddleControls : MonoBehaviour {

    private float minX, maxX, targetX = 0; 
    private float PaddleWidthHalf;
    private float BallRadius;

    public float moveSpeed = 25; 

    public PaddleState currentState
    {
        private set
        {
            _currentState = value;
            EventManager.TriggerEvent(value == PaddleState.Launching ? "StartLaunch" : "StartPlay", gameObject);
            if (ball.transform.position.z < gameObject.transform.position.z) Debug.LogWarning("ball not spawned in correct position");
            if (value == PaddleState.Launching) ball.GetComponent<Collider>().isTrigger = true;
            else ball.GetComponent<Collider>().isTrigger = false;
        }
        get { return _currentState; }

    }
    private PaddleState _currentState;

    [HideInInspector]
    public GameObject ball;

    public enum PaddleState
    {
        Playing,
        Launching
    }

    /// <summary>
    /// gets width/height information from objects in the scene needed for this script to work properly
    /// </summary>
    private void SetBoundaries()
    {
        if (!ball) SpawnBall(); //just for safety
        WallScript[] _walls = FindObjectsOfType<WallScript>();
        GameObject leftWall = Array.Find(_walls, a => a.thisWall == WallScript.walls.left).gameObject;
        GameObject rightWall = Array.Find(_walls, a => a.thisWall == WallScript.walls.right).gameObject;

        minX = leftWall.transform.position.x + leftWall.GetComponent<BoxCollider>().bounds.size.x / 2.0f;
        maxX = rightWall.transform.position.x - rightWall.GetComponent<BoxCollider>().bounds.size.x / 2.0f;
        PaddleWidthHalf = gameObject.GetComponent<BoxCollider>().bounds.size.x / 2.0f;
        BallRadius = ball.GetComponent<SphereCollider>().radius;

        minX += PaddleWidthHalf;
        maxX -= PaddleWidthHalf;
        
    }
    void DestroyBall()
    {
        EventManager.TriggerEvent("BallDestroyed", ball);
        GameObject.Destroy(ball);
        ball = null;
    }

    public void SpawnBall()
    {
        if (ball == null)
        {
            ball = GameObject.Instantiate(Resources.Load("prefabs/ball")) as GameObject;
            ball.transform.position = this.transform.position + new Vector3(0, 0, ball.GetComponent<SphereCollider>().radius + 0.5f);
            EventManager.TriggerEvent("BallSpawn", ball);
        }
        else Debug.LogError("currently doesnt support 'multi-balls' yet");
    }
	// Use this for initialization
	void OnEnable() {
        SpawnBall();
        SetBoundaries();
        currentState = PaddleState.Launching;
        EventManager.StartListening("HoldClick", OnInput);
        EventManager.StartListening("DoubleClick", ShootBall);  //change 'DoubleClick' to any other event in case of change
        EventManager.StartListening("BallBottomDeath", BallDied);
    }

    void OnDisable()
    {
        EventManager.StopListening("HoldClick", OnInput);
        EventManager.StopListening("DoubleClick", ShootBall);
        EventManager.StopListening("BallBottomDeath", BallDied);
    }


    /// <summary>
    /// movement
    /// </summary>
    /// <param name="g"></param>
    /// <param name="f"></param>
    void OnInput(GameObject g, float f)
    {
        Vector2 mousepos = Input.mousePosition;
        Vector3 worldMousepos = transform.position;

        //raycast to check the world-X-position of the mouse
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity))
        {
            worldMousepos = hit.point;
        }

        //if the target X for the paddle is not allowed(insided wall / outside level) move X to closest allowed value
        targetX = worldMousepos.x;
        if (targetX < minX) targetX = minX;
        if (targetX > maxX) targetX = maxX;
    }


    void ShootBall(GameObject g, float f)
    {
        if(currentState != PaddleState.Launching) { Debug.LogWarning("Cannot shoot ball during play"); return; }
        ball.GetComponent<Rigidbody>().velocity = (ball.transform.position - gameObject.transform.position).normalized;
        currentState = PaddleState.Playing;
        ball.GetComponent<FixedSpeed>().ResetSpeed();
        EventManager.TriggerEvent("BallShoot", ball);
    }

    void LaunchUpdate()
    {
        if (!ball) SpawnBall();
        if (currentState != PaddleState.Launching) return;

        if (ball.transform.position.x - BallRadius < transform.position.x - PaddleWidthHalf)
        {
            ball.transform.position = new Vector3(transform.position.x - PaddleWidthHalf + BallRadius, ball.transform.position.y, ball.transform.position.z);
            EventManager.TriggerEvent("BallMoveWithPaddle", ball, 1);
        }
        if (ball.transform.position.x + BallRadius > transform.position.x + PaddleWidthHalf)
        {
            ball.transform.position = new Vector3(transform.position.x + PaddleWidthHalf - BallRadius, ball.transform.position.y, ball.transform.position.z);
            EventManager.TriggerEvent("BallMoveWithPaddle", ball, -1);
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
        LaunchUpdate();
       
	}

    void BallDied(GameObject ball, float f)
    {
        GameManager.instance.LoseEnergy(10);
        DestroyBall();
        SpawnBall();
        currentState = PaddleState.Launching;
        
    }


}
