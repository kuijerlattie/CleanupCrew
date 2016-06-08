using UnityEngine;
using System.Collections;

public class BlobScript : MonoBehaviour {

    [HideInInspector]
    public RodScript.rodtype blobType { private set; get; }

    private Vector3 startDirection;
    private float moveSpeed = 1;


    public static Vector3 GetRandomSpawnPos
    {
        get
        {
            GameObject spawnObject = GameObject.FindGameObjectWithTag("BlobSpawnPos");
            if(spawnObject == null)
            {
                Debug.LogError("no object with tag: 'BlobSpawnPos', cannot spawn blobs on correct position");
                return Vector3.zero;
            }
            Vector3 spawnAreaSize = spawnObject.GetComponent<BoxCollider>().bounds.size /2f;
            float rndX = Random.Range(spawnObject.transform.position.x - spawnAreaSize.x, spawnObject.transform.position.x + spawnAreaSize.x);
            float rndZ = Random.Range(spawnObject.transform.position.z - spawnAreaSize.z, spawnObject.transform.position.z + spawnAreaSize.z);
            return new Vector3(rndX, 0.5f, rndZ);
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
        if(c.collider.gameObject.GetComponent<BallScript>() != null)
        {
            EventManager.TriggerEvent("BallHitBlob", gameObject);
        }

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.GetComponent<BallScript>() != null)
        {
            EventManager.TriggerEvent("BallHitBlob", gameObject);
        }

    }


}
