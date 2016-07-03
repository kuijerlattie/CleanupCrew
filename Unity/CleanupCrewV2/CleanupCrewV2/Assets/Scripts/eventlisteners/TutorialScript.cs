using UnityEngine;
using System.Collections;

public class TutorialScript : BaseGamestate {

    public override void StartState()
    {
        EventManager.StartListening("BallMoveWithPaddle", CheckMove);
        EventManager.StartListening("BallShoot", CheckShoot);
        EventManager.StartListening("BallHitRod", CheckHitRod);
        EventManager.StartListening("BallHitBlob", CheckHitBlob);
        EventManager.StartListening("BlobDestroyed", CheckHitBlob); //in case the player fails to kill the tutorial blob.... it continues anyway
        

        HandContainer = new GameObject("HandContainer");
        HandContainer.transform.position += Vector3.up;

        outlineShader = new Material(Shader.Find("Outlined/Silhouette Only"));
        HandObject = Resources.Load("prefabs/UI/HandV2");
        //valvemanager.instance.AddValveOutline();    //only for testing
        //GameManager.instance.SetState(GameManager.gamestate.Breakout);    //testing only
        //GameManager.instance.SetState(GameManager.gamestate.BossIntermission);    //testing only
    }

    RodScript[] rods = null;
    BlobScript[] blobs = null;
    GameObject HandContainer = null;
    GameObject swipeObject = null;
    GameObject doubletapObject = null;
    Material outlineShader = null;
    Material[] doubleMat = null;
    Object HandObject = null;

    bool blobShader = false;
    bool rodShader = false;


    IEnumerator AddOutLine(GameObject g)
    {
        Renderer R = g.GetComponent<Renderer>();
        
        R.materials = doubleMat == null ? new Material[] { R.materials[0], outlineShader } : doubleMat;
        if (doubleMat == null) doubleMat = R.materials;
        yield return null;
    }

    void Update()
    {
        if(!hasMoved)
        {
            if(swipeObject == null)
            {
                swipeObject = GameObject.Instantiate(HandObject) as GameObject;
                swipeObject.transform.parent = HandContainer.transform;
                Animation a = swipeObject.GetComponent<Animation>();
                a.wrapMode = WrapMode.Loop;
                a.Play("Slide");
                //spawn swiping graphic prefab
            }

            return;
        }
        if (!hasShot)
        {
            if (swipeObject != null) GameObject.Destroy(swipeObject);

            if (doubletapObject == null)
            {
                doubletapObject = GameObject.Instantiate(HandObject) as GameObject;
                doubletapObject.transform.parent = HandContainer.transform;
                Animation a = doubletapObject.GetComponent<Animation>();
                a.wrapMode = WrapMode.Loop;
                a.Play("Tap");
                //spawn doubletap graphic prefab
            }
            return;
        }
        if (!hasHitRod)
        {
            if (doubletapObject != null) GameObject.Destroy(doubletapObject);
            if (swipeObject != null) GameObject.Destroy(swipeObject);
            if (rods == null)
            {
                
                if (!rodShader)
                {
                    rods = FindObjectsOfType<RodScript>();
                    foreach (RodScript rod in rods)
                    {
                        AddOutLine(rod.gameObject);
                        //rod.gameObject.GetComponent<Renderer>().materials = new Material[] { rod.gameObject.GetComponent<Renderer>().materials[0], outlineShader };
                    }
                }
                rodShader = true;
            }
            return;
        }
        if (!hasHitBlob)
        {
            if(rods != null)
            {
                foreach (RodScript rod in rods)
                {
                    rod.gameObject.GetComponent<Renderer>().materials = new Material[] { rod.gameObject.GetComponent<Renderer>().materials[0] };
                }
                rodShader = false;
                BlobScript.InitSpawnLocations();
                BlobScript.Spawn(BlobScript.spawnLocations[2].transform.position, BlobScript.BehaviourType.none);
            }
            rods = null;

            if (!blobShader)
            {
                blobs = FindObjectsOfType<BlobScript>();
                foreach (BlobScript blob in blobs)
                {
                    blob.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials = new Material[] { blob.gameObject.GetComponent<Renderer>().materials[0], outlineShader };
                }
                blobShader = true;
            }

            return;
        }

        GameManager.instance.SetState(GameManager.gamestate.BreakoutIntermission);
    }


    bool hasMoved = false;

    void CheckMove(GameObject g, float f)
    {
        hasMoved = true;
    }

    bool hasShot = false;

    void CheckShoot(GameObject g, float f)
    {
        hasMoved = true;
        hasShot = true;
    }

    bool hasHitRod = false;

    void CheckHitRod(GameObject g, float f)
    {
        hasHitRod = true;
    }

    bool hasHitBlob = false;

    void CheckHitBlob(GameObject g, float f)
    {
        hasHitBlob = true;
    }

    public override void EndState()
    {
        PaddleControls PC = FindObjectOfType<PaddleControls>();
        PC.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        PC.ResetBall();
    }
}
