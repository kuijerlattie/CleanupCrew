using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

    bool pressedOnce = false;
    public Rect spawnLocation;
    public int AmountOfSpheres = 20;   //set in inspector probably
	// Use this for initialization
	void Start () {
        SpawnSpheres.SpawnMultipleSpheres(spawnLocation, AmountOfSpheres);  //TODO move this to other script
        GameSettings.ApplySettings();   //TODO move this to other script
    }

    void Reset()
    {
        pressedOnce = false;
    }

    static public bool GetInput()
    {
        //currently mouse, replace by touchInput later
        return Input.GetMouseButtonDown(0);
    }
    
	
	// Update is called once per frame
	void Update () {
	    if(GetInput() && !pressedOnce)
        {
            GameObject.FindObjectOfType<GameManager>().ResetTimer();
            pressedOnce = true;
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpawnSpheres.SpawnSphere(mousepos, true);
        }
	}
}
