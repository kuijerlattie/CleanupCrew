using UnityEngine;
using System.Collections;

public class MagneticPaddle : MonoBehaviour {


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void Magnetic()
    {
        RandomMovement random;
        GameObject paddle;
        
        paddle = GameObject.FindGameObjectWithTag("paddle");
        random = FindObjectOfType<RandomMovement>();

        FixedSpeed[] speed;
        speed = FindObjectsOfType<FixedSpeed>();

        for (int i = 0; i < speed.Length; i++)
        {
            random.OverrideDirection(paddle.transform.position);
        }

    }

   /* static public void RandomMovement()
    {
        RandomMovement random;
        GameObject paddle;

        paddle = GameObject.FindGameObjectWithTag("paddle");
        random = FindObjectOfType<RandomMovement>();

        FixedSpeed[] speed;
        speed = FindObjectsOfType<FixedSpeed>();

        for (int i = 0; i < speed.Length; i++)
        {
            
        }
    }*/
}
