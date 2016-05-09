using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallScript : MonoBehaviour {

    int points = 0;
    int power = 100;

    public float startingSpeed = 10; //is this variable only for commercial or can we actually use it?

    public Text tpoints; 
    public Text tpower;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 1).normalized * 1000);
	}
	
	// Update is called once per frame
	void Update () {
        startingSpeed += 0.005f;
        if (startingSpeed > 25) startingSpeed = 25;
        Debug.Log(startingSpeed);
        GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized;
        Debug.Log(GetComponent<Rigidbody>().velocity);
        GetComponent<Rigidbody>().velocity *= startingSpeed;
        //Debug.Log(GetComponent<Rigidbody>().velocity);

        //Debug.Log(vel);

        tpoints.text = "Points: " + points;
        tpower.text = "Power: " + power;
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.ToString() == "uranium")
        {
            points += 5;
        }

        if (collision.collider.tag.ToString() == "bottom wall")
        {
            power -= 10;
        }
    }
}
