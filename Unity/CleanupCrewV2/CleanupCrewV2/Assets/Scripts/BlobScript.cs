using UnityEngine;
using System.Collections;

public class BlobScript : MonoBehaviour {

    [HideInInspector]
    public RodScript.rodtype blobType { private set; get; }

    private Vector3 startDirection;
    private float moveSpeed = 1;
    private int health = 1;

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

        EventManager.TriggerEvent("BlobSpawn", newBlob);
        return newBlob;
    }
    // Use this for initialization
    void Start () {

        startDirection = -Vector3.forward;


        GetComponent<Rigidbody>().velocity = startDirection * moveSpeed;
	}

    void Update()
    {
        transform.position += GetBehaviourVector();

    }

    Vector3 GetBehaviourVector()
    {
        //TODO what behaviours?
        return Vector3.zero;    //nothing designed for it yet
        Vector3 vec = Vector3.zero;
        vec.x = Mathf.Sin(transform.position.z);
        vec *= Time.deltaTime * 20f;
        return vec;
    }


    /// <summary>
    /// currently using trigger instead otherwise ball bounces off after collecting a blob
    /// </summary>
    /// <param name="c"></param>
    void OnCollisionEnter(Collision c)
    {
        if (c.collider.gameObject.tag == "Ball")
        {
            EventManager.TriggerEvent("BallHitBlob", gameObject);
        }

    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        EventManager.TriggerEvent("BlobKill", gameObject);
        Destroy(gameObject);
    }

}
