using UnityEngine;
using System.Collections.Generic;

public class SpawnSpheres
{

    static public GameObject SpawnSphere(Vector3 position)
    {
        position.y = 0;
        GameObject spawnedSphere = GameObject.Instantiate(Resources.Load("Prefabs/Ball") as GameObject);
        spawnedSphere.transform.position = position;
        return spawnedSphere;
    }

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
