using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    bool pressedOnce = false;
    public Rect spawnLocation;
    public int AmountOfSpheres = 100;   //set in inspector probably
	// Use this for initialization
	void Start () {
        SpawnSpheres.SpawnMultipleSpheres(spawnLocation, AmountOfSpheres);
	}

    void Reset()
    {
        pressedOnce = false;
    }

    static public bool GetInput()
    {
        return Input.GetMouseButtonDown(0);
    }
    
	
	// Update is called once per frame
	void Update () {
	    if(GetInput() && !pressedOnce)
        {
            pressedOnce = true;
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousepos);
            SpawnSpheres.SpawnSphere(mousepos, true);
        }
	}
}
