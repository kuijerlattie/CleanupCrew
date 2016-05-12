using UnityEngine;
using System.Collections;

public class ScalePowerUp : MonoBehaviour {
    
    GameManager manager;

	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<GameManager>();
    }
	
	// Update is called once per frame
	void Update () {
	if(Input.GetKeyUp(KeyCode.S))
        {
            foreach (GameObject p in manager.paddles)
            {
                p.transform.localScale -= new Vector3(5, 0, 0);
            }
            
        }

    if(Input.GetKeyUp(KeyCode.B))
        {
            foreach (GameObject p in manager.paddles)
            {
                p.transform.localScale += new Vector3(5, 0, 0);
            }
        }
	}
}
