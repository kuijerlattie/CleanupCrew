using UnityEngine;
using System.Collections;

public class PaddleRotationScript : MonoBehaviour {

    private Vector3 _desiredDirection;
    private Vector3 _currentDirection;
    private float desiredAngle = 0;
    float paddleDistanceToCenter = GameSettings.PaddleDistanceS;
    GameObject paddle;
    private float _angleToMove = 0.0f;
    float RotationSpeed = GameSettings.PaddleRotationS;
    public readonly float InputMaxDistance = GameSettings.TouchBarSizeS;  //percent of the screen (y-axis) on the bottom that is clickable, readonly because Bug where it otherwise always changes to 25
    private Vector3 oldMousePos = Vector3.zero;
	// Use this for initialization
	void Start () {
        paddle = gameObject.transform.GetChild(0).gameObject;  //assumes paddle is the first child of this script.
        SetPaddleToDistance();
	}

    void SetPaddleToDistance()
    {
        Vector3 newpos = Vector3.zero;
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
        if (Input.mousePosition.y < Screen.height / 100f * InputMaxDistance && Input.GetMouseButton(0) && ((Input.mousePosition - oldMousePos).magnitude > 0.03f || (Input.mousePosition - oldMousePos).magnitude < -0.03f))
        {
            if (oldMousePos != Vector3.zero)
            {
                float onedegreeInScreenSize = (float)Screen.width/360f;
                float degreesToRotate = (Input.mousePosition - oldMousePos).magnitude / onedegreeInScreenSize;
                degreesToRotate *= Input.mousePosition.x < oldMousePos.x ? 1f : -1f;

                gameObject.transform.Rotate(Vector3.up, degreesToRotate);
                 _desiredDirection = Quaternion.Euler(0, degreesToRotate, 0) * _currentDirection;
                CalculateAngle();
            }
            oldMousePos = Input.mousePosition;

        }
        else oldMousePos = Vector3.zero;
     
        //MoveTo(_desiredDirection);   
    }

    /// <summary>
    /// used when the rotation should not be instant, but gradually moves to the target rotation(rotation of the direction vector)
    /// </summary>
    /// <param name="direction"></param>
    void MoveTo(Vector3 direction)
    {
        float stepRotation = RotationSpeed * Time.deltaTime;
        if (_angleToMove < stepRotation && _angleToMove > -stepRotation) return;   //TODO use epsilon or something probably
        CalculateAngle();
        gameObject.transform.Rotate(Vector3.up, _angleToMove < 0 ? stepRotation : -stepRotation);
    }
    
}
