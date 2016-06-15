using UnityEngine;
using System.Collections;

public class BlobScript : MonoBehaviour {

    [HideInInspector]
    public RodScript.rodtype blobType { private set; get; }

    private Vector3 startDirection;
    private float moveSpeed = 1;
    private int health = 1;
    private bool useBehaviour = true;

    public float behaviourOffset = 0;  // is set later

    public static GameObject[] spawnLocations = null;

    private float TimeAlive = 0;
    private float StartBehaviourAfter = 0.5f;  //after spawning wait x seconds before using behaviour
    private bool currentDirection = true;   //used as an int
    /// <summary>
    /// stop the blob behaviour for x amount of seconds, used to stop them getting stuck against walls trying to continue their behaviour
    /// </summary>
    /// <param name="seconds"></param>
    public void StopBehaviour(float seconds)
    {
        useBehaviour = false;
        StartCoroutine(StartBehaviour(seconds));
        
    }

    private IEnumerator StartBehaviour(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        useBehaviour = true;
    }

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
        newBlob.GetComponent<BlobScript>().currentBehaviour = RandomBehaviour;  //TODO random
        return newBlob;
    }

    private static BehaviourType RandomBehaviour
    {
        get
        {
            BehaviourType[] rodtypeValues = (BehaviourType[])System.Enum.GetValues(typeof(BehaviourType));
            return rodtypeValues[UnityEngine.Random.Range(0, rodtypeValues.GetLength(0))];
        }
    }


/// <summary>
/// spawn blob with a chosen type
/// </summary>
/// <param name="position"></param>
/// <param name="Type"></param>
/// <returns></returns>
public static GameObject Spawn(Vector3 position, BehaviourType behaviour)
    {
        GameObject newBlob = baseSpawn(position);
        newBlob.GetComponent<BlobScript>().blobType = RodScript.RandomType;
        newBlob.GetComponent<BlobScript>().currentBehaviour = behaviour;
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
        behaviourOffset = Time.realtimeSinceStartup;

        GetComponent<Rigidbody>().velocity = startDirection * moveSpeed;
	}

    void Update()
    {
        if (StartBehaviourAfter >= 0) StartBehaviourAfter -= Time.deltaTime;
        TimeAlive += Time.deltaTime;
        if (useBehaviour && StartBehaviourAfter < 0)
            transform.position += GetBehaviourVector() * 0.2f;

    }

    public BehaviourType currentBehaviour = BehaviourType.none;
    public enum BehaviourType
    {
        none,
        sin,
        leftRightBounce

    }

    Vector3 GetBehaviourVector()
    {

        Vector3 vec = Vector3.zero;
        float z = transform.position.z;

        switch(currentBehaviour)
        {
            case BehaviourType.none:
                break;
            case BehaviourType.sin:
                vec.x = Mathf.Sin(TimeAlive * 5f + behaviourOffset);    //Sin maybe performance issues later
                break;
            case BehaviourType.leftRightBounce:
                vec.x = currentDirection ? 1 : -1;
                break;
        }
        
  

        return vec;
    }


    /// <summary>
    /// currently using trigger instead otherwise ball bounces off after collecting a blob
    /// </summary>
    /// <param name="c"></param>
    void OnCollisionEnter(Collision c)
    {
        if(c.collider.gameObject.name == "WallAndCollider" || c.collider.gameObject.GetComponent<RodScript>())
            currentDirection = !currentDirection;

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

    /// <summary>
    /// not used? 
    /// </summary>
    void Die()
    {
        EventManager.TriggerEvent("BlobKill", gameObject);
        Destroy(gameObject);
    }

}
