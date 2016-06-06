using UnityEngine;
using System.Collections;

public class SlowEnemies : MonoBehaviour {


    static public void SlowBlobs()
    {
        FixedSpeed[] speed;
        speed = FindObjectsOfType<FixedSpeed>();

        for(int i = 0; i < speed.Length; i++)
        {
            speed[i].Speed = GameSettings.BlobSpeedS * GameSettings.PowerupSlowEnemiesMultiplierS;
        }
    }

    static public void NormalSpeed()
    {
        FixedSpeed[] speed;
        speed = FindObjectsOfType<FixedSpeed>();

        for (int i = 0; i < speed.Length; i++)
        {
            speed[i].Speed = GameSettings.BlobSpeedS;
        }
    }
}
