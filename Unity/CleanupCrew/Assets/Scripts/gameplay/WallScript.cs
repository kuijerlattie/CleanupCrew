using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

    public bool isTutorial = false;
    public float wallDamage = 10;
    private bool bounce = true;
	// Use this for initialization
	void Start () {
        wallDamage = GameSettings.WallPowerDamageS;
        bounce = GameSettings.BounceWallS;
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
                if(!bounce) GameObject.Destroy(collision.collider.gameObject);
            }
            else GameObject.FindObjectOfType<TutorialPhase>().HitWall();
        }

        if (collision.collider.gameObject.tag == "Blob") // check if collision is with blob
        {
            if(!isTutorial)
                GameObject.FindObjectOfType<GameManager>().power -= wallDamage;
                if (!bounce) GameObject.Destroy(collision.collider.gameObject);
        }
    }
}
