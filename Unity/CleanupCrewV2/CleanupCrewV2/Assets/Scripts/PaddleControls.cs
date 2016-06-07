using UnityEngine;
using System.Collections;

public class PaddleControls : MonoBehaviour {

    float minX, maxX, targetX = 0;
    float PaddleWidthHalf;
    float moveSpeed = 15;

    private void SetBoundaries()
    {
        //TODO get info from the scene
        minX = -10;
        maxX = 10;
        Vector3 paddleSize = gameObject.GetComponent<BoxCollider>().bounds.size;
        PaddleWidthHalf = paddleSize.x / 2.0f;

        minX += PaddleWidthHalf;
        maxX -= PaddleWidthHalf;
        
    }
	// Use this for initialization
	void OnEnable() {
        SetBoundaries();
        EventManager.StartListening("HoldClick", OnInput);
        EventManager.StartListening("DoubleClick", ShootBall);  //change 'DoubleClick' to any other event in case of change
    }

    void OnDisable()
    {
        EventManager.StopListening("HoldClick", OnInput);
        EventManager.StopListening("DoubleClick", ShootBall);
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

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity))
        {
            worldMousepos = hit.point;
        }

        targetX = worldMousepos.x;
        if (targetX < minX) targetX = minX;
        if (targetX > maxX) targetX = maxX;
    }


    void ShootBall(GameObject g, float f)
    {
        //TODO shoot ball code
        EventManager.StopListening("DoubleClick", ShootBall);   //so it only works once
    }

	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), Time.deltaTime * moveSpeed);
	}
}
