using UnityEngine;
using System.Collections;

public class EnergyPowerUp : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void Energy()
    {
        GameManager manager;
        manager = FindObjectOfType<GameManager>();

        manager.power += 10;
        if (manager.power > 100) manager.power = 100;
    }
}
