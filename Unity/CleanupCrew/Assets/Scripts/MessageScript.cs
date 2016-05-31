using UnityEngine;
using System.Collections;

/// <summary>
/// used for tutorial messages to explain the game
/// </summary>
public class MessageScript : MonoBehaviour {

    float TimeUntilDestroy = 3.0f;
    private float currentTimer;
	// Use this for initialization
	void Start () {
        currentTimer = TimeUntilDestroy;
	}
	
	// Update is called once per frame
	void Update () {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0) GameObject.Destroy(gameObject);
	}

    /// <summary>
    /// spawns a prefab in the Resourses folder that will display an image/text and dissappear after set time
    /// </summary>
    /// <param name="path"></param>
    public static void SpawnMessage(string path)
    {
        GameObject prefab = GameObject.Instantiate(Resources.Load(path)) as GameObject;
        if(prefab.GetComponent<MessageScript>() == null)
        {
            Debug.LogWarning("no 'MessageScript' attachted to: " + path + " adding default script to it");
            prefab.AddComponent<MessageScript>();
        }
    }
}
