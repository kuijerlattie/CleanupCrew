using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;


public class EventManager : MonoBehaviour {

    private Dictionary<string, UnityEvent<GameObject, float>> eventDictionary;

    void OnDestroy()
    {
       
    }

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogWarning("There needs to be a EventManager script on a gameobject in your scene, making one now");
                }
                else
                {
                    eventManager.Init();
                }

            }
            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent<GameObject, float>>();
        }
    }

	public static void StartListening(string eventname, UnityAction<GameObject, float> listener)
    {
        UnityEvent<GameObject, float> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventname, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GameEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventname, thisEvent);
        }
    }

    public static void StopListening(string eventname, UnityAction<GameObject, float> listener)
    {
        if (eventManager == null) return; //anti error when eventmanager is gone
        UnityEvent<GameObject, float> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventname, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }


    }

    public static void TriggerEvent(string eventname, GameObject g = null, float f = 0)
    {
        UnityEvent<GameObject,float> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventname, out thisEvent))
        {
            thisEvent.Invoke(g, f); 
        }
    }
}
