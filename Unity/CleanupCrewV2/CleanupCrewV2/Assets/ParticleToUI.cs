using UnityEngine;
using System.Collections;

public class ParticleToUI : MonoBehaviour {

    Vector3 UIWorldPosition = new Vector3(-33, 18, 0);
    float speed = 15;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, UIWorldPosition, Time.deltaTime * speed);
        if (Vector3.Distance(transform.position, UIWorldPosition) < Mathf.Epsilon)
            GameObject.Destroy(gameObject);
	}
}
