using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// used for tutorial messages to explain the game / show tips
/// </summary>
public class MessageScript : MonoBehaviour {

    [HideInInspector]
    public float TimeUntilDestroy = 0;
    private float currentTimer;
    [HideInInspector]
    public Func<bool> DestroyMethod = null;

    [HideInInspector]
    public static GameObject lastMessage = null;
	// Use this for initialization
	void Start () {
        currentTimer = TimeUntilDestroy;
	}
	
	// Update is called once per frame
	void Update () {
        if (DestroyMethod != null && DestroyMethod()) GameObject.Destroy(gameObject);

        if (TimeUntilDestroy == 0) return;
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0) GameObject.Destroy(gameObject);
	}

    /// <summary>
    /// spawns a prefab in the Resourses folder that will display an image/text and dissappear after 'destroymethod' returns true
    /// </summary>
    /// <param name="time"></param> destroy after x seconds
    public static void SpawnMessage(string path, float time)
    {
        GameObject prefab = GameObject.Instantiate(Resources.Load(path)) as GameObject;
        if(lastMessage != null)
        {
            GameObject.Destroy(prefab);
            return;
        }
        if (prefab.GetComponent<MessageScript>() == null)
        {
            Debug.LogWarning("no 'MessageScript' attachted to: " + path + " adding default script to it");
            MessageScript m = prefab.AddComponent<MessageScript>();
            m.TimeUntilDestroy = time;
        }
        lastMessage = prefab;
    }

    /// <summary>
    /// spawns a prefab in the Resourses folder that will display an image/text and dissappear after 'destroymethod' returns true
    /// </summary>
    /// <param name="destroyMethod"></param> destroy message if 'destroyMethod' returns true
    public static void SpawnMessage(string path, Func<bool> destroyMethod)
    {
        GameObject prefab = GameObject.Instantiate(Resources.Load(path)) as GameObject;
        if (lastMessage != null)
        {
            GameObject.Destroy(prefab);
            return;
        }
        if (prefab.GetComponent<MessageScript>() == null)
        {
            Debug.LogWarning("no 'MessageScript' attachted to: " + path + " adding default script to it");
            prefab.AddComponent<MessageScript>();
        }
        prefab.GetComponent<MessageScript>().DestroyMethod = destroyMethod;
        lastMessage = prefab;
    }
}
