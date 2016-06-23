using UnityEngine;
using System.Collections;

public class PwrupRotation : MonoBehaviour {

    float timer = 0.025f;
    float rotTimer;
    GameObject pwrUp;

	// Use this for initialization
	void Start () {
        pwrUp = FindObjectOfType<PwrupRotation>().gameObject;
        rotTimer = timer;
        pwrUp.transform.Rotate(Vector3.right, 90);
    }
	
	// Update is called once per frame
	void Update () {
       // rotTimer -= Time.deltaTime;
       /* if(rotTimer <= 0)
        {
            pwrUp.transform.Rotate(Vector3.right, -10);
            rotTimer = timer;
        }
        */
    }
}
