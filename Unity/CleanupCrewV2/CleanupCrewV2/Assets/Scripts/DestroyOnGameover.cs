using UnityEngine;
using System.Collections;

public class DestroyOnGameover : MonoBehaviour {
    
	void Start ()
    {
        EventManager.StartListening("GameOver", OnGameOver);
	}

    void OnDisable()
    {
        EventManager.StopListening("GameOver", OnGameOver);
    }

    void OnGameOver(GameObject g, float f)
    {
        EventManager.TriggerEvent("ExplodePlz", gameObject);
    }
}
