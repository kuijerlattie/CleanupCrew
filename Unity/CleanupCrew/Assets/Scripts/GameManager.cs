using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    const float GENERATIONTIME = 5; //3 minutes
    const float CLEANINGTIME = 60;
    float gameTimer = 15;
    bool timerPaused = false;

    gamestate currentState;


   
    public gamestate StartstateOverride = gamestate.Tutorial;

    public float expectedPlaytimeInSeconds = 300;

    public int points;
    public int power;
    float elapsedTime = 0;
    float idleTimer = 0;


    //add all objects related to States as child to this, this is deleted after every state switch
    GameObject currentStateObject = null;

    [SerializeField]
    Text pointText;
    [SerializeField]
    Text powerText;

    public Rect spawnLocation;

    public enum gamestate
    {
        Tutorial,
        Cleanup,
        Battle,
        Bossfight
    }


    GameObject g;

    [SerializeField] 
    Text timertext;
    [SerializeField]
    GameObject breakoutObjects;

    void SetState(gamestate state)
    {
        GameObject.Destroy(currentStateObject);

        switch(currentState)
        {
            case gamestate.Tutorial:
                EndTutorial();
                break;
            case gamestate.Cleanup:
                EndCleanup();
                break;
            case gamestate.Battle:
                EndBattle();
                break;
            case gamestate.Bossfight:
                EndBossfight();
                break;
            default:
                Debug.Log("WARNING, tried to switch to not existing state");
                break;
        }
        currentState = state;
        switch (state)
        {
            case gamestate.Tutorial:
                StartTutorial();
                break;
            case gamestate.Cleanup:
                StartCleanup();
                break;
            case gamestate.Battle:
                StartBattle();
                break;
            case gamestate.Bossfight:
                StartBossfight();
                break;
            default:
                break;
        }
    }

    //used to initiate the tutorial actors / props / game rules
    void StartTutorial()
    {

    }

    //used to delete/cleanup
    void EndTutorial()
    {

    }

    //used to initiate the cleanup actors / props / game rules
    void StartCleanup()
    {
        currentStateObject = new GameObject("cleanupPhaseObject");
        CleanupPhase cleanup = currentStateObject.AddComponent<CleanupPhase>();
        cleanup.Start();
        
    }

    //used to delete/cleanup
    void EndCleanup()
    {
        currentStateObject.GetComponent<CleanupPhase>().Stop();
    }

    //used to initiate the battle actors / props / game rules
    void StartBattle()
    {
        currentStateObject = new GameObject("battlePhaseObject");
        BattlePhase battle = currentStateObject.AddComponent<BattlePhase>();
        battle.Start();
    }

    //used to delete/cleanup
    void EndBattle()
    {
        currentStateObject.GetComponent<BattlePhase>().Stop();
    }

    //used to initiate the bossfight actors / props / game rules
    void StartBossfight()
    {

    }

    //used to delete/cleanup
    void EndBossfight()
    {

    }

    void UpdateHud()
    {
        pointText.text = "points: " + points;
        powerText.text = "power: " + power;
    }


    bool messageShown = false; //for debugging

    void Start()
    {
        SetState(StartstateOverride);
    }

	// Update is called once per frame
	void Update () {

        

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= expectedPlaytimeInSeconds && !messageShown) //mostly for debug reasons
        {
            messageShown = true;
            Debug.Log("game has been running for longer than the expected playtime!");
        }
    }
}
