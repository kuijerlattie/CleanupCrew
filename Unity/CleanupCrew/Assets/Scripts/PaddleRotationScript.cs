using UnityEngine;
using System.Collections;

public class PaddleRotationScript : MonoBehaviour {

    Vector3 _desiredDirection;
    Vector3 _currentDirection;
    float paddleDistanceToCenter = 18.0f;
    GameObject paddle;
    float _angleToMove = 0.0f;
    float RotationSpeed = 360;    //degrees per second
    float InputMaxDistance = 50;  //percent of the screen (y-axis) on the bottom that is clickable
    Vector3 oldMousePos = Vector3.zero;
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
        if (Input.mousePosition.y < Screen.height / 100f * InputMaxDistance && Input.GetMouseButton(0) && oldMousePos != Input.mousePosition)
        {
            //Screen.width/360f
            if (oldMousePos != Vector3.zero)
                _desiredDirection = (Quaternion.AngleAxis(((float)Screen.width) * 2f * ((oldMousePos.x - Input.mousePosition.x) / (float)Screen.width), Vector3.up) * _currentDirection).normalized;
            CalculateAngle();
            Debug.Log((oldMousePos.x - Input.mousePosition.x) * ((float)Screen.width / 360f));
            oldMousePos = Input.mousePosition;

        }
        else oldMousePos = Vector3.zero;
   
     
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
