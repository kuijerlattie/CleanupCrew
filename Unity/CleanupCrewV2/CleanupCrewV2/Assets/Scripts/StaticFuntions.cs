using UnityEngine;
using System.Collections;

public class StaticFuntions : MonoBehaviour
{

    public static GameObject SpawnParticle(string name, Vector3 position) //documented
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Particles/" + name)) as GameObject;
        g.transform.position = position;
        return g;
    }

    public static GameObject SpawnParticleToUI(string name, Vector3 position, ParticleToUI.UITarget UI = ParticleToUI.UITarget.Barrel) //documented
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Particles/" + name)) as GameObject;
        g.transform.position = position;
        ParticleToUI PTUI = g.AddComponent<ParticleToUI>();
        PTUI.currentTarget = UI;
        PTUI.Init();
        DestroyParticle DesP = g.GetComponent<DestroyParticle>();
        if (DesP != null) DesP.StayInScene = true;
        return g;
    }
}
