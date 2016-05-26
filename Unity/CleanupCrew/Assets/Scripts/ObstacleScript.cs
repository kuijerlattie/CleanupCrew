using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {

	// Use this for initialization
    /// <summary>
    /// spawns a obstacle on your selected position.
    /// </summary>
    /// <param name="obstacle">the obstacle to spawn</param>
    /// <param name="position">the position to spawn</param>
    /// <returns>the obstacle that was created</returns>
	static public GameObject SpawnObstacle(GameObject obstacle, Vector3 position)
    {
        return (GameObject)GameObject.Instantiate(obstacle, position, Quaternion.identity);
    }

    static public GameObject SpawnObstacle(GameObject obstacle, Vector3 position, float despawnTime)
    {
        GameObject o = (GameObject)GameObject.Instantiate(obstacle, position, Quaternion.identity);
        //set timer to destroy the obstacle;
        return o;
    }

    
}
