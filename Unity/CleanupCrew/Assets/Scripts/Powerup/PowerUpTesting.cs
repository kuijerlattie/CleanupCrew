using UnityEngine;
using System.Collections;

public class PowerUpTesting : MonoBehaviour {

    public int paddleAmount;
    public bool paddle;
    public bool magnetic;
    public bool slow;
    public bool smallEnemies;
    public bool bigger, smaller;

    PowerupManager manager;
    // Use this for initialization
    void Start() {
        manager = FindObjectOfType<PowerupManager>();

        
    }
	// Update is called once per frame
	void Update () {
        if (paddle)
        {
            for (int i = 0; i < paddleAmount; i++)
            {
                manager.ActivatePowerup(PowerupManager.PowerupType.MultiPaddle);
                paddleAmount -= 1;
            }
        }

        if(magnetic)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.Magnetic);
            magnetic = false;
        }

        if(slow)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.SlowEnemies);
            slow = false;
        }

        if(smallEnemies)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.SmallerEnemies);
            smallEnemies = false;
        }

        if(bigger)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.BiggerPaddle);
            bigger = false;
        }

        if(smaller)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.SmallerPaddle);
            smaller = false;
        }
    }
}
