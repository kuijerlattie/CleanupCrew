using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class DestroyParticle : MonoBehaviour {

    ParticleSystem PS;
    public bool StayInScene = false;
	// Use this for initialization
	void Start () {
        PS = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!PS.IsAlive() && !StayInScene) GameObject.Destroy(gameObject);
	}
}
