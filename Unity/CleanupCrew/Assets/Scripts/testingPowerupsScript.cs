using UnityEngine;
using System.Collections;

public class testingPowerupsScript : MonoBehaviour {

    int i = 0;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            switch (i)
            {
                case 1:
                    SI.SpawnPowerup(PowerupManager.PowerupType.BiggerPaddle);
                    break;
                case 2:
                    SI.SpawnPowerup(PowerupManager.PowerupType.SmallerPaddle);
                    break;
                case 3:
                    SI.SpawnPowerup(PowerupManager.PowerupType.SmallerEnemies);
                    break;
                case 4:
                    SI.SpawnPowerup(PowerupManager.PowerupType.MoreEnergy);
                    break;
                default:
                    SI.SpawnPowerup();
                    break;
            }
        }

        i++;
        if (Input.GetKeyDown(KeyCode.O))
        {
            SI.StopAllPowerups();
        }
	}
}
