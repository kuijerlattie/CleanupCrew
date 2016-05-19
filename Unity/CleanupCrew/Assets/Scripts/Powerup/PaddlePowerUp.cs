using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PaddlePowerUp : MonoBehaviour {


	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
 
            
	}

    static public void SpawnPaddles()
    {
        GameObject LevelCenter;
        GameObject paddle;
        GameManager manager;

        LevelCenter = FindObjectOfType<PaddleRotationScript>().gameObject;
        manager = FindObjectOfType<GameManager>();
        paddle = GameObject.FindGameObjectWithTag("paddle");

        manager.paddles.Add(Instantiate(paddle));
        manager.paddles[manager.paddles.Count - 1].transform.SetParent(LevelCenter.transform);
        manager.paddles[manager.paddles.Count - 1].transform.localPosition = manager.paddles[0].transform.localPosition;
        manager.paddles[manager.paddles.Count - 1].transform.rotation = manager.paddles[0].transform.rotation;
        manager.paddles[manager.paddles.Count - 1].transform.localScale = manager.paddles[0].transform.localScale;

    }

    static public void RemovePaddle()
    {
        GameObject LevelCenter;
        GameObject paddle;
        GameManager manager;

        LevelCenter = FindObjectOfType<PaddleRotationScript>().gameObject;
        manager = FindObjectOfType<GameManager>();
        paddle = GameObject.FindGameObjectWithTag("paddle");

        for(int i = 1; i < manager.paddles.Count; i++)
        {
            GameObject.Destroy(manager.paddles[i]);
            manager.paddles.RemoveAt(1);
        }
    }

    static public void CalculatePosition()
    {
        GameObject LevelCenter;
        GameObject paddle;
        GameManager manager;

        LevelCenter = FindObjectOfType<PaddleRotationScript>().gameObject;
        manager = FindObjectOfType<GameManager>();
        paddle = GameObject.FindGameObjectWithTag("paddle");
        //manager.paddles.Add(paddle);

        float radius = 18f;
        
            float dot = Vector3.forward.x * paddle.transform.forward.x + Vector3.forward.z * paddle.transform.forward.z;      // dot product
            float det = Vector3.forward.x * paddle.transform.forward.z - Vector3.forward.z * paddle.transform.forward.x;      // determinant
            float angle = Mathf.Atan2(det, dot) * (180f / Mathf.PI);
        

        for (int i = 0; i < manager.paddles.Count; i++){

       float x = LevelCenter.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
       float z = LevelCenter.transform.position.z + radius * Mathf.Sin (angle * Mathf.Deg2Rad);

       manager.paddles[i].transform.position = new Vector3(x, LevelCenter.transform.position.y,z);
       angle += (360/manager.paddles.Count);
       manager.paddles[i].transform.LookAt(Vector3.zero);
        }
    
    }

}
