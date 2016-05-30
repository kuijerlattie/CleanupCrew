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

        if(manager.power < 100)
        {
            if(manager.power % 10 != 0)
            {
                manager.power += (10 - manager.power % 10);
            }
            else
            manager.power += 10;
        }
        
    }
}
