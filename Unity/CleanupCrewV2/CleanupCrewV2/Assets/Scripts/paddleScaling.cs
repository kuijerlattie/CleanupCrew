using UnityEngine;
using System.Collections;

public class paddleScaling : MonoBehaviour {

    Mesh paddle;
    SkinnedMeshRenderer skinned;
    float skinnedSpeed = 1f;
    int blendShapeCount;
    PwrupManager manager;
    float wider = 0, narrower = 0;
    bool asd = false;
    float timer, timer2;

	// Use this for initialization
	void Start () {
        paddle = FindObjectOfType<PaddleControls>().GetComponent<Mesh>();
        skinned = FindObjectOfType<SkinnedMeshRenderer>();
        blendShapeCount = paddle.blendShapeCount;
        manager = FindObjectOfType<PwrupManager>();

        timer = 2;
        timer2 = timer;
    }
	
	// Update is called once per frame
	void Update () {
        if (wider < 100)
        {
            skinned.SetBlendShapeWeight(1, wider);
            wider += skinnedSpeed;
        }

        if(asd)
        {
            timer2 -= Time.deltaTime;
            if(timer2 < 0)
            {
                if(narrower < 100)
                {
                    skinned.SetBlendShapeWeight(0, narrower);
                    narrower += skinnedSpeed;
                    asd = false;
                    timer2 = timer;
                }
                
            }
        }
    }
}
