using UnityEngine;
using System.Collections;

public class PowerUpTesting : MonoBehaviour {

    private int paddleAmount = GameSettings.AmountOfPaddlesS;
    private bool paddle = true;
    private bool magnetic = GameSettings.MagneticS;
    private bool slow = GameSettings.SlowS;
    private bool smallEnemies = GameSettings.SmallEnemiesS;
    private bool bigger = GameSettings.BigPaddleS, smaller = GameSettings.SmallPaddleS;

    PowerupManager manager;
    // Use this for initialization
    void Start() {
        

    }
	// Update is called once per frame
	void Update () {
        manager = FindObjectOfType<PowerupManager>();
        if (paddle)
        {
            for (int i = 1; i < paddleAmount; i++)
            {
                manager.ActivatePowerup(PowerupManager.PowerupType.MultiPaddle);
                paddleAmount -= 1;
            }
        }

        if (magnetic)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.Magnetic);
            magnetic = false;
        }

        if (slow)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.SlowEnemies);
            slow = false;
        }

        if (smallEnemies)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.SmallerEnemies);
            smallEnemies = false;
        }

        if (bigger)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.BiggerPaddle);
            bigger = false;
        }

        if (smaller)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.SmallerPaddle);
            smaller = false;
        }
    }
}
