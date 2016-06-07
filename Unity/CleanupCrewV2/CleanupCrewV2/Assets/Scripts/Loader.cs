using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameObject gameManager;
    public GameObject eventManager;

	void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
        if (EventManager.instance == null)
            Instantiate(eventManager);
    }
}
