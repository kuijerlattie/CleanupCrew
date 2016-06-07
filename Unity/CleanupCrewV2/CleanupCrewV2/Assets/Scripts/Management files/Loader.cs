using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject gameManager;
    public GameObject eventManager;

	void Awake()
    {
        //always do the eventmanager first!
        if (EventManager.instance == null)
            Instantiate(eventManager);
        if (GameManager.instance == null)
            Instantiate(gameManager);
    }
}
