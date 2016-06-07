using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PaddleRotationScript : MonoBehaviour {

    private Vector3 _desiredDirection;
    private Vector3 _currentDirection;
    private float desiredAngle = 0;
    float paddleDistanceToCenter;
    GameObject paddle;
    private float _angleToMove = 0.0f;
    float RotationSpeed;
    private bool useSlider = true;  //true: new controls, use the slider at the bottom to move the paddle,
                                    //false: uses old controls, tap where u want the paddle to go, EDIT: currently doesnt work

    public bool doubleClickToShoot = false;
    public bool clickPaddleToShoot = true;
    bool buttonToShoot = false;


    //percent of the screen (y-axis) on the bottom that is clickable, readonly because Bug where it otherwise always changes to 25
    public float InputMaxDistance {get; private set;} 
    
    private Vector3 oldMousePos = Vector3.zero;
	// Use this for initialization
	void Start () {
        paddleDistanceToCenter = GameSettings.PaddleDistanceS;
        RotationSpeed = GameSettings.PaddleRotationS;
        InputMaxDistance = GameSettings.TouchBarSizeS;
        useSlider = !GameSettings.OldControlsS;

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
        //_angleToMove /= (float) GameSettings.AmountOfPaddlesS;
    }
	


    /*
    void Update()
    {
        DoInput();
    }

    
    void DoInput()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        int LayerMaskk = 1 << 13;
        if (EventSystem.current.IsPointerOverGameObject()) return; //UI is part of eventsystem so it wont move the paddle when clicking on UI

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, LayerMaskk))
        {
            _desiredDirection = hit.point;
            GameObject test = GameObject.CreatePrimitive(PrimitiveType.Cube);
            test.transform.position = hit.point;
        }
    }*/


    
	// Update is called once per frame
	void Update () {

        bool mouseButton = Input.GetMouseButton(0);
        bool mouseButtonDown = Input.GetMouseButtonDown(0);
        if (Input.GetMouseButtonDown(0))
        {
            int layermask = 1 << 14;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity, layermask))
            {
                Debug.Log(hit.collider.gameObject);
                if (clickPaddleToShoot && hit.collider.gameObject.name == "paddle")
                {
                    //GameObject.FindObjectOfType<PaddleShoot>().Shoot();
                }
            }
        }
        _currentDirection = -paddle.transform.forward;//(gameObject.transform.position - paddle.transform.localPosition).normalized;
        if (useSlider?( Input.mousePosition.y < Screen.height / 100f * InputMaxDistance && mouseButton && ((Input.mousePosition - oldMousePos).magnitude > 0.03f || (Input.mousePosition - oldMousePos).magnitude < -0.03f)) : Input.GetMouseButton(0))
        {
            if (oldMousePos != Vector3.zero)
            {
                float onedegreeInScreenSize = (float)Screen.width/360f;
                float degreesToRotate = useSlider ? (Input.mousePosition - oldMousePos).magnitude / onedegreeInScreenSize
                    : 0;
                
                    
                degreesToRotate *= Input.mousePosition.x < oldMousePos.x ? 1f : -1f;

                if (useSlider)
                {
                    gameObject.transform.Rotate(Vector3.up, degreesToRotate);
                    _desiredDirection = Quaternion.Euler(0, degreesToRotate, 0) * _currentDirection;
                }
                if (!useSlider)
                {
                    int LayerMaskk = 1<<13;
                    //if (EventSystem.current.IsPointerOverGameObject()) return; //UI is part of eventsystem so it wont move the paddle when clicking on UI
                    // _desiredDirection = Input.mou
                    RaycastHit hit;
                    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, LayerMaskk))
                    {
                        //transform.LookAt(hit.point);
                        
                        if(clickPaddleToShoot && hit.collider.gameObject.name == "paddle" && mouseButtonDown )
                        {
                            //GameObject.FindObjectOfType<PaddleShoot>().Shoot();
                        }
                        hit.point = new Vector3(hit.point.x, 0, hit.point.z);
                    _desiredDirection = hit.point;
                    }
                }
            
                CalculateAngle();
            }
            oldMousePos = Input.mousePosition;

        }
        else oldMousePos = Vector3.zero;
     
        if(!useSlider) MoveTo(_desiredDirection);   
    }
    

    /// <summary>
    /// used when the rotation should not be instant, but gradually moves to the target rotation(rotation of the direction vector)
    /// </summary>
    /// <param name="direction"></param>
    void MoveTo(Vector3 direction)
    {
       // transform.rotation *= Quaternion.FromToRotation(_currentDirection, direction);
       Quaternion totalRotation = Quaternion.FromToRotation(_currentDirection, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, totalRotation*transform.rotation, Time.deltaTime * RotationSpeed);
        //transform.Rotate()
        /*
        //TODO somehow the minimup step possible is Pi * (Rotationspeed /~180)
        CalculateAngle();
        float stepRotation = RotationSpeed * Time.deltaTime;
        float minStep = Mathf.PI * (RotationSpeed/ 225f);   //Mathf.epsilon
        if (_angleToMove < minStep && _angleToMove > -minStep) return;
        if (_angleToMove < stepRotation && _angleToMove > -stepRotation)
        {
            //gameObject.transform.Rotate(Vector3.up, -_angleToMove);
            //CalculateAngle();
            //_desiredDirection = _currentDirection;
            return;  
        }
        
        gameObject.transform.Rotate(Vector3.up, _angleToMove < 0 ? stepRotation : -stepRotation);
        */
    }
    
}
