using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaddlePowerUp : MonoBehaviour {

    public GameObject LevelCenter;
    GameObject paddle;
    List<GameObject> paddles = new List<GameObject>();

	// Use this for initialization
	void Start () {
        paddle = GameObject.FindGameObjectWithTag("paddle");
        paddles.Add(paddle);
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            SpawnPaddles();
            CalculatePosition();
        }
	}

    void SpawnPaddles()
    {
        paddles.Add(Instantiate(paddle));
        paddles[paddles.Count - 1].transform.SetParent(LevelCenter.transform);
        paddles[paddles.Count - 1].transform.localPosition = paddles[0].transform.localPosition;
        paddles[paddles.Count - 1].transform.rotation = paddles[0].transform.rotation;
        paddles[paddles.Count - 1].transform.localScale = paddles[0].transform.localScale;

    }

    void CalculatePosition()
    {
        
       float radius = 18f;
       float angle = 0;
        for (int i = 0; i< paddles.Count; i++) {
   float x = LevelCenter.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
   float z = LevelCenter.transform.position.z + radius * Mathf.Sin (angle * Mathf.Deg2Rad);

   paddles[i].transform.position = new Vector3(x, LevelCenter.transform.position.y,z);
   angle += (360/paddles.Count);
            paddles[i].transform.LookAt(Vector3.zero);
        }
    
    }
}
