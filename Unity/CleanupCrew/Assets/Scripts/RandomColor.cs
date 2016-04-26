using UnityEngine;
using System.Collections;

public class RandomColor : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material.color = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
