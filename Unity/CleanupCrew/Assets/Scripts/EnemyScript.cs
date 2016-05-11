using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EnemyScript : MonoBehaviour {

    float health = 100; //percent
    float damagePerHit = 25; //percent

    bool stoppedAtCenter = false;
    float _currentSpawnTime = 0;
    float _SPAWNTIME = 3.0f;
    public PointScript.goalType enemytype;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(stoppedAtCenter) _currentSpawnTime -= Time.deltaTime;
        if(_currentSpawnTime <= 0 && stoppedAtCenter)
        {
            _currentSpawnTime = _SPAWNTIME;
            Vector3 randomPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            SpawnSpheres.SpawnProjectile(transform.position + randomPos * gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x/2f, randomPos, enemytype);
        }

        if (gameObject.transform.position.magnitude < 0.5f && !stoppedAtCenter)
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
        
        if(col.collider.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {
            health -= damagePerHit; //TODO change color
            GameObject.Destroy(col.collider.gameObject);
        }
    }
}
