using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EnemyScript : MonoBehaviour {

    float health = 100; //percent
    float damagePerHit = 25; //percent

    bool stoppedAtCenter = false;
    float _currentSpawnTime = 0;
    float _SPAWNTIME = 3.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        _currentSpawnTime -= Time.deltaTime;
        if(_currentSpawnTime <= 0)
        {
            SpawnSpheres.SpawnSphere(transform.position);
        }

        if (gameObject.transform.position.magnitude < 0.1f && !stoppedAtCenter)
        {
            stoppedAtCenter = true;
            gameObject.GetComponent<FixedSpeed>().enabled = false;
            Rigidbody body = gameObject.GetComponent<Rigidbody>();
            body.velocity = Vector3.zero;
            body.isKinematic = true;
        }
	    if(health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
	}

    void OnCollisionEnter(Collision col)
    {
        
        if(col.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {
            health -= damagePerHit; //TODO change color
        }
    }
}
