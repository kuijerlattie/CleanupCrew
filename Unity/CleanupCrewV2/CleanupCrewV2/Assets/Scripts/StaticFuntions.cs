using UnityEngine;
using System.Collections;

public class StaticFuntions : MonoBehaviour {

    public static GameObject SpawnParticle(string name, Vector3 position) //documented
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Particles/" + name)) as GameObject;
        g.transform.position = position;
        return g;
    }
}
