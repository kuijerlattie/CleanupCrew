using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TESTONPCSCRIPT : MonoBehaviour {

    public Text FPS;
    public Text FPSUNDER30;

    int framesUnder30 = 0;
    int frameCount = 0;
    float nextUpdate = 0.0f;
    float fps = 0.0f;
    float updateRate = 4.0f;  // 4 updates per sec.

    // Use this for initialization
    void Start () {
        QualitySettings.vSyncCount = 1;
        nextUpdate = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        FPS.text = "fps: " + (int)fps;
        FPSUNDER30.text = "fps < 30: " + framesUnder30;
   
       

        frameCount++;
        if (Time.time > nextUpdate)
        {
            nextUpdate += 1.0f / updateRate;
            fps = frameCount * updateRate;
            frameCount = 0;

            if (fps != 0 && fps < 30)
            {
                framesUnder30++;
            }
        }
    }


   


}
