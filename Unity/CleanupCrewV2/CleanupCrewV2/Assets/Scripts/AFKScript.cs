using UnityEngine;
using System.Collections;

public class AFKScript : MonoBehaviour {

    float AFKTimer = 0.0f;
    float AFKWarningTime = 10.0f;
    float AFKEndGame = 30.0f;
    bool GaveWarning = false;
    bool EndedGame = false;

	// Use this for initialization
	void OnEnable ()
    {
        EventManager.StartListening("Click", OnScreenTouch);
        EventManager.StartListening("HoldClick", OnScreenTouch);
	}
	
    void OnDisable()
    {
        EventManager.StopListening("Click", OnScreenTouch);
        EventManager.StopListening("HoldClick", OnScreenTouch);
    }

	// Update is called once per frame
	void Update () {
        if (GameManager.instance.CurrentGamestate == GameManager.gamestate.GameOver) return;

        AFKTimer += Time.deltaTime;
        if (AFKTimer >= AFKWarningTime && !GaveWarning)
        {
            EventManager.TriggerEvent("PlayerAFK", null, AFKTimer);
            GaveWarning = true;
        }

        if (AFKTimer >= AFKEndGame && !EndedGame)
        {
            EventManager.TriggerEvent("PlayerAFKGameOver", null, AFKTimer);
            EndedGame = true;
            EventManager.StopListening("Click", OnScreenTouch);
            EventManager.StopListening("HoldClick", OnScreenTouch);
            GameManager.instance.SetState(GameManager.gamestate.GameOver);
        }
	}

    void OnScreenTouch(GameObject g, float f)
    {
        AFKTimer = 0.0f;
        GaveWarning = false;
    }
}
