using UnityEngine;
using System.Collections;
using System;

public class DoOnDestroy : MonoBehaviour {


    public string eventname = "Diededed";
	void OnDestroy()
    {
        if(!GameManager.IsQuitting)
            EventManager.TriggerEvent(eventname, gameObject);
    }


    
}
