using UnityEngine;
using System.Collections;

public class StickyBall : MonoBehaviour {

    GameObject ball;

	// Use this for initialization
	void Start () {
        ball = GameObject.FindGameObjectWithTag("Ball");
        if(ball.GetComponent<StickyBall>() == null)
        {
            ball.gameObject.AddComponent<StickyBall>();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
