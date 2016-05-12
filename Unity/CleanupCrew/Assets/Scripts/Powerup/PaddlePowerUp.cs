using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaddlePowerUp : MonoBehaviour {

    public GameObject LevelCenter;
    GameObject paddle;
    GameManager manager;

	// Use this for initialization
	void Start () {
        manager = FindObjectOfType<GameManager>();
        paddle = GameObject.FindGameObjectWithTag("paddle");
        manager.paddles.Add(paddle);
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
        manager.paddles.Add(Instantiate(paddle));
        manager.paddles[manager.paddles.Count - 1].transform.SetParent(LevelCenter.transform);
        manager.paddles[manager.paddles.Count - 1].transform.localPosition = manager.paddles[0].transform.localPosition;
        manager.paddles[manager.paddles.Count - 1].transform.rotation = manager.paddles[0].transform.rotation;
        manager.paddles[manager.paddles.Count - 1].transform.localScale = manager.paddles[0].transform.localScale;

    }

    void CalculatePosition()
    {
        
       float radius = 18f;
       float angle = 0;

       for (int i = 0; i < manager.paddles.Count; i++){

       float x = LevelCenter.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
       float z = LevelCenter.transform.position.z + radius * Mathf.Sin (angle * Mathf.Deg2Rad);

       manager.paddles[i].transform.position = new Vector3(x, LevelCenter.transform.position.y,z);
       angle += (360/manager.paddles.Count);
       manager.paddles[i].transform.LookAt(Vector3.zero);
        }
    
    }
}
