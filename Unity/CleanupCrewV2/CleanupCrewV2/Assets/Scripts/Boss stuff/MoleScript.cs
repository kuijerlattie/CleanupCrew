using UnityEngine;

public class MoleScript : BossBase {

    float abovegroundtime = 10f; //excluding dig time
    float undergroundtime = 5f; //excluding dig time
    float timer = 0f;
    bool fightstarted = false;

    Animator anim;

    bool digparticlesStarted = false;

    GameObject bossarea;

    public ParticleSystem diggingparticle;

    Vector3 targetlocation = new Vector3(0, 0, 0);

    public molestate state = molestate.aboveground; //make not public again
    molestate oldstate;

    public enum molestate
    {
        underground,
        diggingUp,
        diggingDown,
        aboveground,
        gothit,
        dead
    }
    
    void SetState(molestate s)
    {
        oldstate = state;
        state = s;
        switch (s)
        {
            case molestate.underground:
                invincible = true;
                break;
            case molestate.diggingUp:
                PlayAnimation("Mole_DigUp",0.5f);
                CameraShake.ScreenShake(1.5f, 0.05f);
                invincible = true;
                //anim.SetTrigger("PlayDigUp");
                break;
            case molestate.diggingDown:
                PlayAnimation("Mole_DigDown",0.5f);
                CameraShake.ScreenShake(1.5f, 0.05f);
                invincible = true;
                //anim.SetTrigger("PlayDigDown");
                break;
            case molestate.aboveground:
                if (oldstate == molestate.gothit && timer < 1.5f)
                    invincible = true;
                else
                    invincible = false;
                break;
            case molestate.gothit:
                EventManager.TriggerEvent("BossHit");
                invincible = true;
                break;
            case molestate.dead:
                CameraShake.ScreenShake(4f, 0.5f);
                PlayAnimation("Mole_Death", 0.5f);
                invincible = true;
                break;
            default:
                break;
        }
    }

	// Use this for initialization
	void Start () {
        hitpoints = 6;
        targetlocation = gameObject.transform.position;
        bossarea = GameObject.Find("BossArea");
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.CurrentGamestate != GameManager.gamestate.Boss) return;
        if (!fightstarted) { fightstarted = true; invincible = false; }
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
            case molestate.gothit:
                GotHit();
                break;
            case molestate.dead:
                DoBossDeath();
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
        if (!AnimationIsPlaying("Mole_DigUp"))
        {
            SetState(molestate.aboveground);
            timer = abovegroundtime;
            StopParticles();
            return;
        }
        
    }

    void DigDown()
    {

        if (!AnimationIsPlaying("Mole_DigDown"))
        {
            SetState(molestate.underground);
            timer = undergroundtime;

            StopParticles();

            //set new location to start digging up
            GetNewBossPosition();
            gameObject.transform.position = targetlocation;

            return;
        }
    }

    void AboveGround()
    {
        if (hitpoints < maxhitpoints * 0.8 || hitpoints < maxhitpoints - 4) //first 20% of hitpoints  or first 4 hits mole will not go down
        {
            if (timer <= 1 && !digparticlesStarted)
            {
                StartParticles();
            }
            if (timer <= 0 && AnimationsFinished())
            {
                //targetlocation = gameObject.transform.position + new Vector3(0, -5, 0);
                Debug.Log("boss new target location = " + targetlocation);
                SetState(molestate.diggingDown);
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
            //targetlocation = gameObject.transform.position + new Vector3(0, 5, 0);
            SetState(molestate.diggingUp);
        }
    }

    void GetNewBossPosition()
    {
        targetlocation.x = Random.Range(bossarea.transform.position.x - bossarea.transform.localScale.x/2, bossarea.transform.position.x + bossarea.transform.localScale.x/2);
        targetlocation.y = 0;
        targetlocation.z = Random.Range(bossarea.transform.position.z - bossarea.transform.localScale.z/2, bossarea.transform.position.z + bossarea.transform.localScale.z/2);
    }

    void StartParticles()
    {
        diggingparticle.transform.position = new Vector3(targetlocation.x, 0, targetlocation.z);
        diggingparticle.Play(true);
        digparticlesStarted = true;
    }

    void StopParticles()
    {
        diggingparticle.Stop(true);
        digparticlesStarted = false;
    }
    
    void DoBossDeath()
    {
        if (AnimationsFinished())
        {
            MoveBossDown();
            if (transform.position.y <= -5f)
            {
                EventManager.TriggerEvent("BossDied", gameObject);
                Destroy(gameObject);
            }
        }
    }


    void MoveBossDown()
    {
        targetlocation.y = -5;
        transform.position = Vector3.MoveTowards(transform.position, targetlocation, 1.0f);
    }

    public override void Die()
    {
        alive = false;
        SetState(molestate.dead);
    }

    public override void OnHit(int hitid)
    {
        if (timer <= 0)
        {
            timer = 2;
            invincible = true;
        }
        switch(hitid)
        {
            case 1:
                PlayAnimation("Mole_Hit", 0.5f);
                break;
            case 2:
                PlayAnimation("Mole_Hit", 0.5f);
                break;
            case 3:
                PlayAnimation("Mole_Hit", 0.5f);
                break;
            default:
                PlayAnimation("Mole_hit", 0.5f); //play nose hit anyways
                break;
        }
        SetState(molestate.gothit);
        CameraShake.ScreenShake(0.5f, 0.15f);
    }

    void GotHit()
    {
        invincible = true;
        if (!AnimationIsPlaying("Mole_Hit") && !AnimationIsPlaying("Mole_TailHitLeft") && !AnimationIsPlaying("Mole_TailHitRight"))
        {
            SetState(oldstate);
        }
    }

    public void Block(int side)
    {
        if (!AnimationsFinished()) return;
        if (side == 0)
            PlayAnimation("Mole_BlockLeft", 2f);
        if (side == 1)
            PlayAnimation("Mole_BlockRight", 2f);
    }

    void PlayAnimation(string name, float speed = 0.5f)
    {
        anim.speed = speed;
        anim.Play(name);
    }

    bool AnimationIsPlaying(string name)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name)) return true;
        return false;
    }
    
    bool AnimationsFinished()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Mole_PrevDone") || anim.GetCurrentAnimatorStateInfo(0).IsName("Mole_Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("Mole_StayDead"))
            return true;
        return false;     
    }
}
