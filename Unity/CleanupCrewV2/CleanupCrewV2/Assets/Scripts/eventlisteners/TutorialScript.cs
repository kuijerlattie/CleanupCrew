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
    }

    RodScript[] rods = null;
    BlobScript[] blobs = null;
    GameObject swipeObject = null;
    GameObject doubletapObject = null;

    void Update()
    {
        if(!hasMoved)
        {
            if(swipeObject == null)
            {
                swipeObject = GameObject.Instantiate(Resources.Load("prefabs/UI/HandV2")) as GameObject;
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
                doubletapObject = GameObject.Instantiate(Resources.Load("prefabs/UI/HandV2")) as GameObject;
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
                rods = FindObjectsOfType<RodScript>();
                foreach(RodScript rod in rods)
                {
                    rod.gameObject.GetComponent<Renderer>().materials = new Material[] { rod.gameObject.GetComponent<Renderer>().materials[0], new Material(Shader.Find("Outlined/Silhouette Only")) };
                }
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
                BlobScript.Spawn(BlobScript.GetRandomSpawnPos, BlobScript.BehaviourType.none);
            }
            rods = null;

            blobs = FindObjectsOfType<BlobScript>();
            foreach (BlobScript blob in blobs)
            {
                blob.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials = new Material[] { blob.gameObject.GetComponent<Renderer>().materials[0], new Material(Shader.Find("Outlined/Silhouette Only")) };
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
        
    }
}
