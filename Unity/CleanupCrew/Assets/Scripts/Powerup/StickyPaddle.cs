using UnityEngine;
using System.Collections;

public class StickyPaddle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void Sticky()
    {
        GameObject[] balls;

        balls = GameObject.FindGameObjectsWithTag("Blob");

        foreach (GameObject i in balls)
        {
            if (i.GetComponent<StickyBall>() == null)
            {
                i.gameObject.AddComponent<StickyBall>();
            }
        }

    }
}
