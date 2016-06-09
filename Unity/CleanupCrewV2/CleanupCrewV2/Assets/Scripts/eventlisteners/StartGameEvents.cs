using UnityEngine;
using System.Collections;

public class StartGameEvents : MonoBehaviour {

	// Use this for initialization
	void OnEnable()
    {
        EventManager.StartListening("StartGame", StartGame);
    }

    void OnDisable()
    {
        EventManager.StartListening("StartGame", StartGame);
    }

    void StartGame(GameObject g, float f)
    {
        GameManager.instance.SetEnergy(100);
        GameManager.instance.SetPoints(0);
    }
}
