using UnityEngine;
using System.Collections;

/// <summary>
/// sudoku after x seconds
/// </summary>
public class DestroyAfterXSeconds : MonoBehaviour {

    public float timeTillDestroy = 9999;
    private float currentTime = 1;
	// Use this for initialization
	void Start () {
        currentTime = timeTillDestroy;
	}
	
	// Update is called once per frame
	void Update () {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0) GameObject.Destroy(gameObject);
	}
}
