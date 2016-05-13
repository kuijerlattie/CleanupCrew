using UnityEngine;
using System.Collections;

public class ScalePowerUp : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}

    static public void ScaleDown()
    {
        GameManager manager;
        manager = FindObjectOfType<GameManager>();

        foreach (GameObject p in manager.paddles)
        {
            p.transform.localScale -= new Vector3(5, 0, 0);
        }

    }

    static public void ScaleUp()
    {
        GameManager manager;
        manager = FindObjectOfType<GameManager>();

        foreach (GameObject p in manager.paddles)
        {
            p.transform.localScale += new Vector3(5, 0, 0);
        }

    }
}
