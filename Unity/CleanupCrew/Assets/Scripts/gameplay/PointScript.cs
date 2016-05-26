using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {

    public enum goalType
    {
        water,
        underground,
        space
    }

    public goalType type;
    int waterpoints = 1;
    int undergroundpoints = 2;
    int spacepoints = 5;
    int waterPower = 1; //not used
    int undergroundPower = 3;   //not used
    int spacePower = 8; //not used

    GameManager manager;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Blob" || collision.collider.gameObject.tag == "Ball") // check if collision is with ball
        {
            switch (type)
            {
                case goalType.water:
                    manager.points += waterpoints;
                    //manager.power -= waterPower;  //why would you lose power here? makes more sense to remove power ONLY when balls hit the wall
                    manager.pointsWater++;
                    break;
                case goalType.underground:
                    manager.points += undergroundpoints;
                    //manager.power -= undergroundPower;
                    manager.pointsUnderground++;
                    break;
                case goalType.space:
                    manager.points += spacepoints;
                    //manager.power -= spacePower;
                    manager.pointsSpace++;  
                    break;
                default:
                    break;
            }

            GameObject.Destroy(collision.collider.gameObject);
        }
    }
}
