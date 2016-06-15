using UnityEngine;
using System.Collections;

public class MoleScript : BossBase {

    float abovegroundtime = 10f; //excluding dig time
    float undergroundtime = 5f; //excluding dig time
    float timer = 0f;

    bool digparticlesStarted = false;

    GameObject bossarea;

    public ParticleSystem diggingparticle;

    Vector3 targetlocation = new Vector3(0, 0, 0);

    public molestate state = molestate.aboveground; //make not public again

    public enum molestate
    {
        underground,
        diggingUp,
        diggingDown,
        aboveground
    }
    

	// Use this for initialization
	void Start () {
        hitpoints = 6;
        targetlocation = gameObject.transform.position;
        bossarea = GameObject.Find("BossArea");
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.CurrentGamestate != GameManager.gamestate.Boss) return;
        invincible = false;
        timer -= Time.deltaTime;
        switch (state)
        {
            case molestate.underground:
                BelowGround();
                break;
            case molestate.diggingUp:
                DigUp();
                break;
            case molestate.diggingDown:
                DigDown();
                break;
            case molestate.aboveground:
                AboveGround();
                break;
            default:
                break;
        }
	}
    /*
    boss mechanics for the mole

    mole comes out of ground, stays for a bit
    after few hits mole goes underground, moves somewhere else
    mole spawns particles above him showing ground moving or shit
    mole comes back up. stays for x seconds (maybe does an attack)
    mole goes back down moves (repeat this pattern till death)
    */

    void DigUp()
    {
        if (gameObject.transform.position == targetlocation)
        {
            state = molestate.aboveground;
            timer = abovegroundtime;
            StopParticles();
            return;
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetlocation, 0.5f);
    }

    void DigDown()
    {
        if (gameObject.transform.position == targetlocation)
        {
            state = molestate.underground;
            timer = undergroundtime;

            StopParticles();

            //set new location to start digging up
            GetNewBossPosition();
            gameObject.transform.position = targetlocation;

            return;
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetlocation, 0.5f);
    }

    void AboveGround()
    {
        if (hitpoints < maxhitpoints * 0.8 || hitpoints < maxhitpoints - 4) //first 20% of hitpoints  or first 4 hits mole will not go down
        {
            if (timer <= 1 && !digparticlesStarted)
            {
                StartParticles();
            }
            if (timer <= 0)
            {
                targetlocation = gameObject.transform.position + new Vector3(0, -5, 0);
                Debug.Log("boss new target location = " + targetlocation);
                state = molestate.diggingDown;
            }
        }
    }

    void BelowGround()
    {
        if (timer <= 1 && !digparticlesStarted)
        {
            StartParticles();
        }

        if (timer <= 0)
        {
            targetlocation = gameObject.transform.position + new Vector3(0, 5, 0);
            state = molestate.diggingUp;
        }
    }

    void GetNewBossPosition()
    {
        targetlocation.x = Random.Range(bossarea.transform.position.x - bossarea.transform.localScale.x/2, bossarea.transform.position.x + bossarea.transform.localScale.x/2);
        targetlocation.z = Random.Range(bossarea.transform.position.z - bossarea.transform.localScale.z/2, bossarea.transform.position.z + bossarea.transform.localScale.z/2);
    }

    void StartParticles()
    {
        diggingparticle.transform.position = new Vector3(targetlocation.x, -0.5f, targetlocation.z);
        diggingparticle.Play();
        digparticlesStarted = true;
    }

    void StopParticles()
    {
        diggingparticle.Stop();
        digparticlesStarted = false;
    }
    
    public override void Die()
    {
        alive = false;
        EventManager.TriggerEvent("BossDied", gameObject);
        Destroy(gameObject);
    }

}
