using UnityEngine;
using System.Collections;

public class SlowEnemies : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
        
	}

	
	// Update is called once per frame
	void Update () {

        

    }

    static public void SlowBlobs()
    {
        FixedSpeed[] speed;
        speed = FindObjectsOfType<FixedSpeed>();

        for(int i = 0; i < speed.Length; i++)
        {
            speed[i].Speed *= 0.5f;
        }
    }

    static public void NormalSpeed()
    {
        FixedSpeed[] speed;
        speed = FindObjectsOfType<FixedSpeed>();

        for (int i = 0; i < speed.Length; i++)
        {
            if(speed[i].Speed < 5)
            {
                speed[i].Speed *= 2.0f;
            }
        }
    }
}
