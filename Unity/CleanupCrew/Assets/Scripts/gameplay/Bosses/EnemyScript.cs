using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EnemyScript : MonoBehaviour {  //TODO make this abstract once all bosses have their own script

    [HideInInspector]
    public float health {get; private set; } //percent

    float damagePerHit;
    public bool AbleToShoot = true;
    bool stoppedAtCenter = false;
    float _currentSpawnTime = 0;
    float _SPAWNTIME;
    public PointScript.goalType enemytype;
    float SpeedMultiplier;
    protected bool useBaseCollider = true;
    protected bool isReady { get; private set; }
    public bool overrideStoppedAtCenter { set { stoppedAtCenter = value; } }
	// Use this for initialization
	protected void BaseStart () {
        health = 100;
        damagePerHit = GameSettings.DamagePerHitTakenS;
        _SPAWNTIME = GameSettings.ProjectileFireSpeedS;
        SpeedMultiplier = GameSettings.ProjectileSpeedMultiplierS;

        GetComponent<Collider>().isTrigger = true;  //fixes where the boss can get bounced away by blobs....
	}

    public void DoDamage(float damage)
    {
        health -= damage;
    }
	
	// Update is called once per frame
	protected void BaseUpdate () {
        _SPAWNTIME = GameSettings.ProjectileFireSpeedS; //only in the update for changing during runtime
        SpeedMultiplier = GameSettings.ProjectileSpeedMultiplierS; //only in the update for changing during runtime
        if (stoppedAtCenter) _currentSpawnTime -= Time.deltaTime;


        //spawn a projectile at a random position around this gameobject going in that same direction
        if(_currentSpawnTime <= 0 && stoppedAtCenter && AbleToShoot)
        {
            _currentSpawnTime = _SPAWNTIME;
            Vector3 randomPos = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            GameObject projectile = SpawnSpheres.SpawnProjectile(transform.position + randomPos * (gameObject.GetComponent<MeshFilter>().mesh.bounds.size.x/4f + 1), randomPos, enemytype);
            projectile.GetComponent<FixedSpeed>().Speed *= SpeedMultiplier;
            projectile.transform.parent = gameObject.transform;
        }


        //stops the gameobject from moving after reaching the center and starts the shooting by setting 'stoppedAtCenter = true'
        if (gameObject.transform.position.magnitude < 0.5f && !stoppedAtCenter)
        {
            GetComponent<Collider>().isTrigger = false;
            stoppedAtCenter = true;
            gameObject.GetComponent<FixedSpeed>().enabled = false;
            Rigidbody body = gameObject.GetComponent<Rigidbody>();
            body.velocity = Vector3.zero;
            body.isKinematic = true;
            isReady = true;
            SetupWhenReady();
        }
	    if(HasDied())
        {
            GameObject.Destroy(gameObject);
        }
	}

    /// <summary>
    /// to do initialisation after the enemy    stopped in the middle/completed animation
    /// </summary>
    protected virtual void SetupWhenReady()
    {
        //override this
    }

    protected virtual bool HasDied()
    {
        if (health <= 0) return true;
        return false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (!useBaseCollider) return;   //if the enemy has special colliders ignore this collider, projectiles will bounce off instead

        if(col.collider.gameObject.layer == LayerMask.NameToLayer("Balls")) //(projectiles are on the 'Balls' layer currently)
        {
            health -= damagePerHit; //TODO change color or something
            GameObject.Destroy(col.collider.gameObject);
        }
    }
}
