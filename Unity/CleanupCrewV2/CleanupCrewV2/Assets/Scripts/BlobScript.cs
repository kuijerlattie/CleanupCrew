using UnityEngine;
using System.Collections;

public class BlobScript : MonoBehaviour {

    [HideInInspector]
    public RodScript.rodtype blobType { private set; get; }

    private Vector3 startDirection;
    private float moveSpeed = 1;

    public static GameObject[] spawnLocations = null;



    private static void InitSpawnLocations()
    {
       spawnLocations = GameObject.FindGameObjectsWithTag("BlobSpawnPos");
        if (spawnLocations.GetLength(0) < 3) Debug.LogError("No spawn points in scene, add tags 'BlobSpawnPos' to objects (atleast 3)");
    }

    void OnDestroy()
    {
        if(!GameManager.IsQuitting)
            EventManager.TriggerEvent("BlobDestroyed", gameObject);
    }
    public static Vector3 GetRandomSpawnPos
    {
        get
        {
            if (spawnLocations == null) InitSpawnLocations();
            return spawnLocations[Random.Range(0, spawnLocations.GetLength(0))].transform.position;
        }
    }
        
    /// <summary>
    /// spawn blob with a random type
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static GameObject Spawn(Vector3 position)
    {
        GameObject newBlob = baseSpawn(position);
        newBlob.GetComponent<BlobScript>().blobType = RodScript.RandomType;
        return newBlob;
    }

  

    /// <summary>
    /// spawn blob with a chosen type
    /// </summary>
    /// <param name="position"></param>
    /// <param name="Type"></param>
    /// <returns></returns>
    public static GameObject Spawn(Vector3 position, RodScript.rodtype Type)
    {
        GameObject newBlob = baseSpawn(position);
        newBlob.GetComponent<BlobScript>().blobType = Type;
        return newBlob;
    }

    private static GameObject baseSpawn(Vector3 position)
    {
        string prefabName = "Blob";
        GameObject newBlob = GameObject.Instantiate(Resources.Load("prefabs/" + prefabName)) as GameObject;
        newBlob.transform.position = position;
        if (position.y != 0.5f) Debug.LogWarning("Warning, spawning not on Y = 0.5");

        EventManager.TriggerEvent("BlobSpawn", newBlob);
        return newBlob;
    }
    // Use this for initialization
    void Start () {

        startDirection = -Vector3.forward;


        GetComponent<Rigidbody>().velocity = startDirection * moveSpeed;
	}
	

    /// <summary>
    /// currently using trigger instead otherwise ball bounces off after collecting a blob
    /// </summary>
    /// <param name="c"></param>
	void OnCollisionEnter(Collision c)
    {
        if(c.collider.gameObject.tag == "Ball")
        {
            EventManager.TriggerEvent("BallHitBlob", gameObject);
        }

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Ball")
        {
            EventManager.TriggerEvent("BallHitBlob", gameObject);
        }

    }


}
