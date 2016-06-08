using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public List<BaseGamestate> stateBasedScripts = new List<BaseGamestate>();

    public enum gamestate
    {
        Start,
        Tutorial,
        BreakoutIntermission,
        Breakout,
        BossIntermission,
        Boss
    }

    public static GameManager instance = null;
    private gamestate gameState = gamestate.Start;


    private int energy = 0;
    private int points = 0;

    private int waterpoints = 0;
    private int earthpoints = 0;
    private int spacepoints = 0;



    void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        InitGame();
        
    }

    void InitGame()
    {
        SetState(gamestate.Start);
    }

    public void SetState(gamestate state)
    {
        EndState();
        switch (state)
        {
            case gamestate.Start:
                StartGame();
                StartIntro();
                break;
            case gamestate.Tutorial:
                StartTutorial();
                break;
            case gamestate.BreakoutIntermission:
                StartBreakoutIntermission();
                break;
            case gamestate.Breakout:
                StartBreakout();
                break;
            case gamestate.BossIntermission:
                StartBossIntermission();
                break;
            case gamestate.Boss:
                StartBoss();
                break;
            default:
                break;
        }
    }

    private void EndState()
    {
       foreach (BaseGamestate m in stateBasedScripts)
        {
            m.EndState();
            Destroy(m);
        }
        stateBasedScripts.Clear();
    }

    void StartGame()
    {
        //everything you make here wont be automaticly removed at the end of a gamestate
        gameObject.AddComponent<InitializeGame>().StartState();
    }

    void StartIntro()
    {
        //game start animation and shit
    }

    void StartTutorial()
    {
        //play tutorial
    }

    void StartBreakoutIntermission()
    { 
        //breakout start animation and shit
    }

    void StartBreakout()
    {
        //start the main gameplay stuff
    }

    void StartBossIntermission()
    {
        //boss start animation and shit
    }

    void StartBoss()
    {
        //actually start the bossfight logics
    }

    public gamestate CurrentGamestate
    { get { return gameState; } }
    
    public int CurrentEnergy
    { get { return energy; } }

    public int CurrentPoints
    { get { return points; } }

    public int CurrentWaterPoints
    { get { return waterpoints; } }

    public int CurrentEarthPoints
    { get { return earthpoints; } }

    public int CurrentSpacePoints
    { get { return spacepoints; } }

    public void AddPoints(int p)
    {
        points += p;
        EventManager.TriggerEvent("GainedPoints", null, p);
    }

    public void AddEnergy(int e)
    {
        energy += e;
        EventManager.TriggerEvent("GainedEnergy", null, e);
    }

    public void LoseEnergy(int e)
    {
        energy -= e;
        EventManager.TriggerEvent("LoseEnergy", null, e);
    }



}
