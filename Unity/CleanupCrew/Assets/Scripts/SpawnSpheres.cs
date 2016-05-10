using UnityEngine;
using System.Collections;

public class SpawnSpheres
{

   
    static public void SpawnSphere(Vector3 position, bool ImmidiateExplode = false)
    {
        position.y = 0;
        GameObject spawnedSphere = GameObject.Instantiate(Resources.Load("Prefabs/Ball") as GameObject);
        spawnedSphere.transform.position = position;
        //spawnedSphere.GetComponent<Explosion>().enabled = ImmidiateExplode;
    }

    
        

    

    static public void SpawnMultipleSpheres(Rect spawnArea, int amount)
    {
        //currently uses square root to get nicely divided positions but is not accurate to 'int amount' , real value is lower
        float Root = Mathf.Sqrt(amount);
        int roundedRoot = (int)Root;
        for (int ix = 0; ix < roundedRoot; ix++)
        {
            for (int iy = 0; iy < roundedRoot; iy++)
            {
                Vector3 spawnloc = new Vector3(spawnArea.width / roundedRoot * ix + spawnArea.x, 0, spawnArea.height / roundedRoot * iy + spawnArea.y);
                SpawnSphere(spawnloc, false);
            }
        }
    }

}
