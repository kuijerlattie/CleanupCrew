using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    const float GENERATIONTIME = 5; //3 minutes
    const float CLEANINGTIME = 60;
    float gameTimer = 15;
    bool timerPaused = false;

    [HideInInspector]
    public gamestate currentState;

    [HideInInspector]
    public List<GameObject> paddles = new List<GameObject>();


   
    public gamestate StartstateOverride = gamestate.Tutorial;

    public float expectedPlaytimeInSeconds = 300;

    public int points;
    public int pointsWater = 0;
    public int pointsUnderground = 0;
    public int pointsSpace = 0;
    public float power;
    GameObject paddle;
    public float elapsedTime = 0;
    public float idleTimer = 0;

    //add all objects related to States as child to this, this is deleted after every state switch
    GameObject currentStateObject = null;
    AbstractPhase currentPhase = null;

    public void ResetPoints()
    {
        pointsWater = 0;
        pointsUnderground = 0;
        pointsSpace = 0;
        //power = 100;
    }

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

    public void SetState(gamestate state)
    {
        GameObject.Destroy(currentStateObject);

        if (currentStateObject != null && currentPhase != null)
        {
            switch (currentState)
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
        currentStateObject = new GameObject("tutorialPhaseObject");
        currentPhase = currentStateObject.AddComponent<TutorialPhase>();
        currentPhase.StartPhase();
    }

    //used to delete/cleanup
    void EndTutorial()
    {
        currentPhase.StopPhase();
    }

    //used to initiate the cleanup actors / props / game rules
    void StartCleanup()
    {
        currentStateObject = new GameObject("cleanupPhaseObject");
        currentPhase = currentStateObject.AddComponent<CleanupPhase>();
        currentPhase.StartPhase();
        
    }

    //used to delete/cleanup
    void EndCleanup()
    {
        currentStateObject.GetComponent<CleanupPhase>().StopPhase();
    }

    //used to initiate the battle actors / props / game rules
    void StartBattle()
    {
        currentStateObject = new GameObject("battlePhaseObject");
        currentPhase = currentStateObject.AddComponent<BattlePhase>();
        currentPhase.StartPhase();
    }

    //used to delete/cleanup
    void EndBattle()
    {
        currentStateObject.GetComponent<BattlePhase>().StopPhase();
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
        timertext.text = elapsedTime.ToString();
        pointText.text = "points: " + Mathf.Round(points);
        powerText.text = "power: " + Mathf.Round(power);
        if (power <= 0 && currentState != gamestate.Tutorial) powerText.text = "power: GAME OVER, POWER REACHED 0";
        
    }


    bool messageShown = false; //for debugging

    private void CheckScriptExistence()
    {
        if (FindObjectOfType<GreyboxMenuScript>() == null) Debug.LogWarning("Missing 'GreyboxMenuScript' unable to switch scenes");
        if (FindObjectOfType<GameSettings>() == null) Debug.LogWarning("Missing 'GameSettings' using default settings");
        if (FindObjectOfType<PowerupManager>() == null) Debug.LogWarning("Missing 'PowerupManager' powerups won't spawn or won't work");
    }

    void Start()
    {
        CheckScriptExistence();
        SetState(StartstateOverride);
        paddle = GameObject.FindGameObjectWithTag("paddle");
        paddles.Add(paddle);
    }

    /// <summary>
    /// Checks if the End-condition has been met for the current phase, and proceed to next phase.
    /// </summary>
    void AutomaticSwitchState()
    {
        if (currentPhase != null && currentPhase.HasEnded()) SetState(currentPhase.nextGamestate);
    }

    // Update is called once per frame
    void Update () {

        UpdateHud();

        elapsedTime += Time.deltaTime;
        if (elapsedTime >= expectedPlaytimeInSeconds && !messageShown) //mostly for debug reasons
        {
            messageShown = true;
            Debug.Log("game has been running for longer than the expected playtime!");
        }

        idleTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            idleTimer = 0;
        }

        AutomaticSwitchState();
    }
}
