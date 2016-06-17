using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PaddleControls : MonoBehaviour {

    private float minX, maxX, targetX = 0; 
    private float PaddleWidthHalf;
    private float BallRadius;

    private float moveSpeed = 75; 

    public PaddleState currentState
    {
        private set
        {
            _currentState = value;
            EventManager.TriggerEvent(value == PaddleState.Launching ? "StartLaunch" : "StartPlay", gameObject);

            foreach (GameObject g in playingBalls)
            {
                g.GetComponent<Collider>().isTrigger = value == PaddleState.Launching ? true : false;
            }
        }
        get { return _currentState; }

    }
    private PaddleState _currentState;


    List<GameObject> playingBalls = new List<GameObject>();

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
        if (playingBalls.Count < 1) SpawnBall(); //just for safety
        WallScript[] _walls = FindObjectsOfType<WallScript>();
        GameObject leftWall = Array.Find(_walls, a => a.thisWall == WallScript.walls.left).gameObject;
        GameObject rightWall = Array.Find(_walls, a => a.thisWall == WallScript.walls.right).gameObject;

        minX = leftWall.transform.position.x + leftWall.GetComponent<BoxCollider>().bounds.size.x / 2.0f;
        maxX = rightWall.transform.position.x - rightWall.GetComponent<BoxCollider>().bounds.size.x / 2.0f;
        PaddleWidthHalf = gameObject.GetComponent<BoxCollider>().bounds.size.x / 2.0f;
        BallRadius = playingBalls[0].GetComponent<SphereCollider>().radius;

        minX += PaddleWidthHalf; //enable to make paddle stop at wall, disable to make it go into the wall half
        maxX -= PaddleWidthHalf; //enable to make paddle stop at wall, disable to make it go into the wall half
        
    }
    void DestroyBall(GameObject ball)
    {
        EventManager.TriggerEvent("BallDestroyed", ball);
        playingBalls.Remove(ball);
        GameObject.Destroy(ball);
        ball = null;
    }

    public GameObject SpawnBall()
    {
        GameObject ball;
        ball = GameObject.Instantiate(Resources.Load("prefabs/ball")) as GameObject;
        ball.transform.position = this.transform.position + new Vector3(0, 0, ball.GetComponent<SphereCollider>().radius + 0.5f);
        EventManager.TriggerEvent("BallSpawn", ball);
        playingBalls.Add(ball);

        return ball;
        
    }

    public void ResetBall()
    {
        GameObject ball;
        while (playingBalls.Count > 0)
        {
            ball = playingBalls[playingBalls.Count - 1];
            playingBalls.Remove(ball);
            Destroy(ball);
        }

        SpawnBall();
        currentState = PaddleState.Launching;
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

        for (int i = playingBalls.Count-1; i > 0; i--)
        {
            GameObject.Destroy(playingBalls[i]);
        }
        playingBalls.Clear();
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
        if (playingBalls.Count > 1) return; //cannot shoot with multiple balls
        GameObject ball = playingBalls[0];
        if(currentState != PaddleState.Launching || GameManager.instance.CurrentGameplaystate != GameManager.gameplaystate.running) { Debug.LogWarning("Cannot shoot ball during play or when gameplay is paused"); return; }
        ball.GetComponent<Rigidbody>().velocity = (ball.transform.position - gameObject.transform.position).normalized;
        currentState = PaddleState.Playing;
        ball.GetComponent<FixedSpeed>().ResetSpeed();
        EventManager.TriggerEvent("BallShoot", ball);
    }

    void LaunchUpdate()
    {
        if (playingBalls.Count < 1) SpawnBall();
        if (currentState != PaddleState.Launching) return;
        GameObject ball = playingBalls[0];
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
        if (playingBalls.Count > 1) {/* when an extra ball dies*/ }
        else GameManager.instance.LoseEnergy(10);

        
        DestroyBall(ball);
        if (playingBalls.Count == 0)
        {
            SpawnBall();
            currentState = PaddleState.Launching;
        }
        
    }


}
