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
        Boss,
        GameOver
    }

    public enum gameplaystate
    {
        paused,
        running
    }

    public static GameManager instance = null;
    private gamestate gameState = gamestate.Start;
    private gameplaystate gameplayState = gameplaystate.paused;


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
        SetGameplayState(gameplaystate.paused);
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
            case gamestate.GameOver:
                GameOver();
                break;
            default:
                break;
        }
    }

    public void SetGameplayState(gameplaystate state)
    {
        switch (state)
        {
            case gameplaystate.paused:
                EventManager.TriggerEvent("PauseGame");
                break;
            case gameplaystate.running:
                EventManager.TriggerEvent("ResumeGame");
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
        SetGameplayState(gameplaystate.paused);
        EventManager.TriggerEvent("StartGame");
    }

    void StartIntro()
    {
        stateBasedScripts.Add(gameObject.AddComponent<IntroScript>());
        stateBasedScripts[stateBasedScripts.Count].StartState();
        SetGameplayState(gameplaystate.paused);
        EventManager.TriggerEvent("StartIntro");
        //game start animation and shit
    }

    void StartTutorial()
    {
        stateBasedScripts.Add(gameObject.AddComponent<TutorialScript>());
        stateBasedScripts[stateBasedScripts.Count].StartState();
        SetGameplayState(gameplaystate.running);
        EventManager.TriggerEvent("StartTutorial");
        //play tutorial
    }

    void StartBreakoutIntermission()
    {
        stateBasedScripts.Add(gameObject.AddComponent<BreakoutIntermissionScript>());
        stateBasedScripts[stateBasedScripts.Count].StartState();
        SetGameplayState(gameplaystate.paused);
        EventManager.TriggerEvent("BreakoutIntermission");
        //breakout start animation and shit
    }

    void StartBreakout()
    {
        stateBasedScripts.Add(gameObject.AddComponent<BreakoutScript>());
        stateBasedScripts[stateBasedScripts.Count].StartState();
        SetGameplayState(gameplaystate.running);
        EventManager.TriggerEvent("StartBreakout");
        //start the main gameplay stuff
    }

    void StartBossIntermission()
    {
        stateBasedScripts.Add(gameObject.AddComponent<BossIntermissionScript>());
        stateBasedScripts[stateBasedScripts.Count].StartState();
        SetGameplayState(gameplaystate.paused);
        EventManager.TriggerEvent("BossIntermission");
        //boss start animation and shit
    }

    void StartBoss()
    {
        stateBasedScripts.Add(gameObject.AddComponent<BossScript>());
        stateBasedScripts[stateBasedScripts.Count].StartState();
        SetGameplayState(gameplaystate.running);
        EventManager.TriggerEvent("StartBoss");
        //actually start the bossfight logics
    }

    void GameOver()
    {
        stateBasedScripts.Add(gameObject.AddComponent<GameOverScript>());
        stateBasedScripts[stateBasedScripts.Count].StartState();
        SetGameplayState(gameplaystate.running);
        EventManager.TriggerEvent("GameOver", null, points);
        //game is over. destroy (explode?) everything
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

    public void LosePoints(int p)
    {
        points -= p;
        EventManager.TriggerEvent("LosePoints", null, p);
    }

    public void SetPoints(int p)
    {
        if (points > 0)
        {
            if (points > p)
                EventManager.TriggerEvent("GainedPoints", null, points - p);
            if (points < p)
                EventManager.TriggerEvent("LosePoints", null, p - points);
        }
        points = p;
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
        if (energy >= 0)
        {
            SetState(gamestate.GameOver);
        }
    }
    
    public void SetEnergy(int e)
    {
        if (energy > 0)
        {
            if (energy > e)
                EventManager.TriggerEvent("LoseEnergy", null, energy - e);
            if (energy < e)
                EventManager.TriggerEvent("GainedEnergy", null, e - energy);
        }
        energy = e;
    }
}
