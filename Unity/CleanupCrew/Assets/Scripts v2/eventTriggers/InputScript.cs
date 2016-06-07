using UnityEngine;
using System.Collections;


/// <summary>
/// if we do need multiTouch, put it here
/// swiping, double tap, long press etc
/// </summary>
public class InputScript : MonoBehaviour {

    private DoubleClick doubleclick;
	// Use this for initialization
	void Start () {
        doubleclick = new DoubleClick();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0)) EventManager.TriggerEvent("Click");
        if (Input.GetMouseButton(0)) EventManager.TriggerEvent("HoldClick");

    }


    private class DoubleClick
    {
        float TimeAtLastClick = 0;
        float MaxTimeBetweenClicks = 0.25f;  //max seconds for it to recognise a double click, NOTE: dont put below 0.2, makes it impossible

        public DoubleClick()
        {
            EventManager.StartListening("Click", CheckDoubleClick);
        }

        ~DoubleClick()
        {
            EventManager.StopListening("Click", CheckDoubleClick);
        }

        void CheckDoubleClick(GameObject g, float f)
        {
            float clickTime = Time.realtimeSinceStartup - TimeAtLastClick;
            if(clickTime <= MaxTimeBetweenClicks)
            {
                EventManager.TriggerEvent("DoubleClick", null, clickTime);
            }
            TimeAtLastClick = Time.realtimeSinceStartup;
        }

    }


}
