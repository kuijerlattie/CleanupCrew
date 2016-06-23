using UnityEngine;
using System.Collections.Generic;

public class UIevents : MonoBehaviour {

    List<float> pointsQueue = new List<float>();
    float timer = 0.3f;
    const float TIMER = 0.3f;

	// Use this for initialization
	void Start () {
        EventManager.StartListening("GainedPoints", GainPoints);
	}

    void OnDisable()
    {
        EventManager.StopListening("GainedPoints", GainPoints);
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
	    if(pointsQueue.Count > 0 && timer <= 0)
        {
            SpawnPopup(pointsQueue[0]);
            timer = TIMER;
        }
	}

    void SpawnPopup(float f)
    {
        GameObject g = GameObject.Instantiate(Resources.Load("prefabs/PopupCanvas")) as GameObject;
        g.GetComponentInChildren<PopupText>().SetValue(f);
        pointsQueue.RemoveAt(0);
    }

    void GainPoints(GameObject g, float f)
    {
        pointsQueue.Add(f);
    }
}
