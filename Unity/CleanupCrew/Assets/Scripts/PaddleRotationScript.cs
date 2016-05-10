using UnityEngine;
using System.Collections;

public class PaddleRotationScript : MonoBehaviour {

    Vector3 _desiredDirection;
    Vector3 _currentDirection;
    float paddleDistanceToCenter = 13.0f;
    GameObject paddle;
    float _angleToMove = 0.0f;
    float RotationSpeed = 360;    //degrees per second
    float InputMaxDistance = 5;  //used to specify how big the 'ring' is you can click/tap
	// Use this for initialization
	void Start () {
        paddle = gameObject.transform.GetChild(0).gameObject;  //assumes paddle is the first child of this script.
        SetPaddleToDistance();
	}

    void SetPaddleToDistance()
    {
        Vector3 newpos = Vector3.zero;
        //Vector3 newpos = gameObject.transform.GetChild(0).position; using current pos
        newpos.z = -paddleDistanceToCenter;
        paddle.transform.localPosition = newpos; 
    }

    void CalculateAngle()
    {
        float dot = _currentDirection.x * _desiredDirection.x + _currentDirection.z * _desiredDirection.z;      // dot product
        float det = _currentDirection.x * _desiredDirection.z - _currentDirection.z * _desiredDirection.x;      // determinant
        _angleToMove = Mathf.Atan2(det, dot) * (180f / Mathf.PI);
    }
	
	// Update is called once per frame
	void Update () {

        _currentDirection = paddle.transform.forward;//(gameObject.transform.position - paddle.transform.localPosition).normalized;
        if (Input.GetMouseButton(0))
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldMousePos.y = gameObject.transform.position.y;
            Vector3 direction = gameObject.transform.position - worldMousePos;
            if(direction.magnitude < paddleDistanceToCenter + InputMaxDistance && direction.magnitude > paddleDistanceToCenter - InputMaxDistance)
            _desiredDirection = direction.normalized;
            CalculateAngle();
        }
        MoveTo(_desiredDirection);
        

    }

    void MoveTo(Vector3 direction)
    {
        float stepRotation = RotationSpeed * Time.deltaTime;
        if (_angleToMove < stepRotation && _angleToMove > -stepRotation) return;   //TODO use epsilon or something probably
        CalculateAngle();
        gameObject.transform.Rotate(Vector3.up, _angleToMove < 0 ? stepRotation : -stepRotation);
    }
    
}
