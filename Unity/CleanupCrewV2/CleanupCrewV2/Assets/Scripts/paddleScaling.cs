using UnityEngine;
using System.Collections;

public class paddleScaling : MonoBehaviour {

    Mesh paddle;
    SkinnedMeshRenderer skinned;
    PwrupManager manager;
    BoxCollider col;
    int blendShapeCount;
    float skinnedSpeed = 1f;
    float timer, timer2;
    float wider = 0, narrower = 0;
    bool nDone = false, wDone = false;

	// Use this for initialization
	void Start () {
        
        skinned = FindObjectOfType<PaddleControls>().GetComponentInChildren<SkinnedMeshRenderer>();
        paddle = skinned.sharedMesh;
        blendShapeCount = paddle.blendShapeCount;
        manager = FindObjectOfType<PwrupManager>();
        col = FindObjectOfType<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {

        if(blendShapeCount > 2)
        {
            if(manager.getWider)
            {
                if(wider <= 100)
                {
                    skinned.SetBlendShapeWeight(1, wider);
                    wider += skinnedSpeed;
                    col.size = new Vector3(1.25f, 1, 1);
                }
               

            }

            if(manager.getNarrower)
            {
                if (narrower <= 100)
                {
                    skinned.SetBlendShapeWeight(0, narrower);
                    narrower += skinnedSpeed;
                    col.size = new Vector3(.75f, 1, 1);
                }

            }

            if(manager.resetSize)
            {
                if(wider >= 0)
                {
                    skinned.SetBlendShapeWeight(1, wider);
                    wider -= skinnedSpeed;
                    FindObjectOfType<PaddleControls>().SetBoundaries();
                    col.size = new Vector3(1, 1, 1);
                }

                if(narrower >= 0)
                {
                    skinned.SetBlendShapeWeight(0, narrower);
                    narrower -= skinnedSpeed;
                    FindObjectOfType<PaddleControls>().SetBoundaries();
                    col.size = new Vector3(1, 1, 1);
                }

                
            }
        }
        
    }
}
