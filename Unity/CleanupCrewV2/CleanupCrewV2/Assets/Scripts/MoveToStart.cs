using UnityEngine;
using System.Collections;

public class MoveToStart : MonoBehaviour {

    public float targetRotation = 0.0f;
    private float currentRotation = 0.0f;
    private float _rotationSpeed = 15f;
    private bool _reachedTarget { get { return Mathf.Abs(currentRotation) >= Mathf.Abs(targetRotation); } }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!_reachedTarget) UpdateMovement();
	}

    private void UpdateMovement()
    {
        //transform.position = Vector3.MoveTowards(transform.position, _localTargetPosition, _moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime * targetRotation < 0? -1f : 1f);
        currentRotation += _rotationSpeed * Time.deltaTime;
    }
}
