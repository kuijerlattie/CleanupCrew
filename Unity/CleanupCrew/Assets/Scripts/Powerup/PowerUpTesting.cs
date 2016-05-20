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

        manager = FindObjectOfType<PowerupManager>();
    }
	// Update is called once per frame
	void Update () {

        paddleAmount = GameSettings.AmountOfPaddlesS;
        magnetic = GameSettings.MagneticS;
        slow = GameSettings.SlowS;
        smallEnemies = GameSettings.SmallEnemiesS;
        bigger = GameSettings.BigPaddleS;
        smaller = GameSettings.SmallPaddleS;
        if (paddle)
        {
            for (int i = 1; i < paddleAmount; i++)
            {
                manager.ActivatePowerup(PowerupManager.PowerupType.MultiPaddle);
                paddleAmount -= 1;
                GameSettings.AmountOfPaddlesS = paddleAmount;
            }
        }

        if (magnetic)
        {
            
            manager.ActivatePowerup(PowerupManager.PowerupType.Magnetic);
            magnetic = false;
            GameSettings.MagneticS = magnetic;
        }

        if (slow)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.SlowEnemies);
            slow = false;
            GameSettings.SlowS = slow;
        }

        if (smallEnemies)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.SmallerEnemies);
            smallEnemies = false;
            GameSettings.SmallEnemiesS = smallEnemies;
        }

        if (bigger)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.BiggerPaddle);
            bigger = false;
            GameSettings.BigPaddleS = bigger;
        }

        if (smaller)
        {
            manager.ActivatePowerup(PowerupManager.PowerupType.SmallerPaddle);
            smaller = false;
            GameSettings.SmallPaddleS = smaller;
        }
    }
}
