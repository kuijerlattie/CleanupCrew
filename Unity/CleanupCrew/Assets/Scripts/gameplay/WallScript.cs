using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

    public bool isTutorial = false;
    public float wallDamage = 10;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Ball") // check if collision is with ball
        {
            if (!isTutorial)
            {
                GameObject.FindObjectOfType<GameManager>().power -= wallDamage;
                GameObject.Destroy(collision.collider.gameObject);
            }
            else GameObject.FindObjectOfType<TutorialPhase>().HitWall();
        }
    }
}
