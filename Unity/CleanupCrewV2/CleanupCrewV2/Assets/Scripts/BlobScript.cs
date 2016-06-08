using UnityEngine;
using System.Collections;

public class BlobScript : MonoBehaviour {

    [HideInInspector]
    public RodScript.rodtype blobType { private set; get; }

    private Vector3 startDirection;
    private float moveSpeed = 5;

        
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
        string prefabName = "Blob"; //TODO change to correct prefab once its there
        GameObject newBlob = GameObject.Instantiate(Resources.Load("prefabs/" + prefabName)) as GameObject;
        newBlob.transform.position = position;
        if (position.y != 0.5f) Debug.LogWarning("Warning, spawning not on Y = 0.5");
        return newBlob;
    }
    // Use this for initialization
    void Start () {

        //random starting direction
        Vector2 randomVec = Random.insideUnitCircle;
        startDirection = new Vector3(randomVec.x, 0, randomVec.y).normalized;


        GetComponent<Rigidbody>().velocity = startDirection * moveSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
