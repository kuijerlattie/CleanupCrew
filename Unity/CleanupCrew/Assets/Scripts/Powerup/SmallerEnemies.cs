using UnityEngine;
using System.Collections;

public class SmallerEnemies : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    static public void SrinkEnemies()
    {
        GameObject[] blobs;

        blobs = GameObject.FindGameObjectsWithTag("Ball");

        for(int i = 0; i < blobs.Length; i++)
        {
            blobs[i].transform.localScale = new Vector3(0.5f, 1, 0.5f);
        }

    }

    static public void ReturnSize()
    {
        GameObject[] blobs;

        blobs = GameObject.FindGameObjectsWithTag("Ball");

        for (int i = 0; i < blobs.Length; i++)
        {
                blobs[i].transform.localScale = new Vector3(1, 1, 1);
            
        }

    }
}
