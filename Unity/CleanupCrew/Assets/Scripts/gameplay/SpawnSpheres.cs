using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnSpheres
{

    /// <summary>
    /// mostly used to avoid instant collision when spawning
    /// </summary>
    /// <param name="position"></param>
    /// <param name="isBlob"></param>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public static IEnumerator SpawnWithDelay(Vector3 position, bool isBlob = true, float seconds = 1)
    {
        yield return new WaitForSeconds(seconds);
        position.y = 0;
        GameObject spawnedSphere = isBlob ? GameObject.Instantiate(Resources.Load("Prefabs/BlobPrefab") as GameObject) : GameObject.Instantiate(Resources.Load("Prefabs/Ball") as GameObject);
        spawnedSphere.transform.position = position;

        yield return null;
    }
    static public GameObject SpawnSphere(Vector3 position, bool isBlob = true)
    {
        position.y = 0;
        GameObject spawnedSphere = isBlob ? GameObject.Instantiate(Resources.Load("Prefabs/BlobPrefab") as GameObject) : GameObject.Instantiate(Resources.Load("Prefabs/Ball") as GameObject);
        spawnedSphere.transform.position = position;
        return spawnedSphere;
    }

    static public GameObject SpawnSphere(Vector3 position, Vector3 direction, bool isBlob = true)
    {
        position.y = 0;
        GameObject spawnedSphere = isBlob? GameObject.Instantiate(Resources.Load("Prefabs/BlobPrefab") as GameObject) : GameObject.Instantiate(Resources.Load("Prefabs/Ball") as GameObject);
        spawnedSphere.GetComponent<RandomMovement>().OverrideDirection(direction);
        spawnedSphere.transform.position = position;
        return spawnedSphere;
    }

    static public GameObject SpawnProjectile(Vector3 position, Vector3 direction, PointScript.goalType goaltype)
    {
        position.y = 0;
        direction.y = 0;
        string prefabPath = "Prefabs/EnemyWaterProjectile";
        switch(goaltype)
        {
            case PointScript.goalType.space:
                prefabPath = "Prefabs/EnemySpaceProjectile";
                break;
            case PointScript.goalType.underground:
                prefabPath = "Prefabs/EnemyGroundProjectile";
                break;
            case PointScript.goalType.water:
                prefabPath = "Prefabs/EnemyWaterProjectile";
                break;
                
        }
        GameObject spawnedSphere = GameObject.Instantiate(Resources.Load(prefabPath) as GameObject);
        spawnedSphere.transform.position = position;
        spawnedSphere.GetComponent<Rigidbody>().velocity = direction;
        
        return spawnedSphere;
    }

    /// <summary>
    /// dont use this
    /// </summary>
    /// <param name="spawnArea"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    static public GameObject[] SpawnMultipleSpheres(Rect spawnArea, int amount)
    {
        List<GameObject> spheres = new List<GameObject>();
        //currently uses square root to get nicely divided positions but is not accurate to 'int amount' , real value is lower
        float Root = Mathf.Sqrt(amount);
        int roundedRoot = (int)Root;
        for (int ix = 0; ix < roundedRoot; ix++)
        {
            for (int iy = 0; iy < roundedRoot; iy++)
            {
                Vector3 spawnloc = new Vector3(spawnArea.width / roundedRoot * ix + spawnArea.x, 0, spawnArea.height / roundedRoot * iy + spawnArea.y);
               spheres.Add(SpawnSphere(spawnloc));
            }
        }
        return spheres.ToArray();
    }

}
