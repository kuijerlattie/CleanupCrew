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
    int waterPower = 1;
    int undergroundPower = 3;
    int spacePower = 8;

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
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Balls")) // check if collision is with ball
        {
            switch (type)
            {
                case goalType.water:
                    manager.points += waterpoints;
                    manager.power -= waterPower;
                    break;
                case goalType.underground:
                    manager.points += undergroundpoints;
                    manager.power -= undergroundPower;
                    break;
                case goalType.space:
                    manager.points += spacepoints;
                    manager.power -= spacePower;    
                    break;
                default:
                    break;
            }

            GameObject.Destroy(collision.collider.gameObject);
        }
    }
}
