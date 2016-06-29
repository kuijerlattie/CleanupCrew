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

    public int CurrentLevel = 0; //should be 0 when building the game for playtests.

    public static GameManager instance = null;
    private gamestate gameState = gamestate.Start;
    private gameplaystate gameplayState = gameplaystate.paused;


    private int energy = 0;
    private int maxenergy = 100;
    private float screenpoints = 0;
    private int points = 0;

    private int Barrelcontent = 0;
    private float screenBarrelContent = 0;
    private int GooNeededForBoss = 10;

    public static bool IsQuitting = false;

    void OnApplicationQuit()
    {
        IsQuitting = true;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
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
        Debug.Log("state switched from " + gameState + " to " + state + ".");
        gameState = state;
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

        Debug.Log("gameplay state switched from " + gameplayState + " to " + state + ".");
        gameplayState = state;
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
        int i = 0;
        foreach (BaseGamestate m in stateBasedScripts)
        {
            m.EndState();
            Destroy(m);
            i++;
        }
        Debug.Log("removed " + i + " state based scripts from the " + gameState + " state");
        stateBasedScripts.Clear();
    }

    void StartGame()
    {
        //everything you make here wont be automaticly removed at the end of a gamestate
        gameObject.AddComponent<InitializeGame>().StartState();
        gameObject.AddComponent<pointevents>();
        gameObject.AddComponent<ParticleEvents>();
        gameObject.AddComponent<AFKScript>();
		gameObject.AddComponent<AudiEvents>();
        gameObject.AddComponent<UIevents>();
        ParticleToUI.SetUIForParticles();
        SetGameplayState(gameplaystate.paused);
        EventManager.TriggerEvent("StartGame");
    }

    void StartIntro()
    {
        SetGameplayState(gameplaystate.paused);
        stateBasedScripts.Add(gameObject.AddComponent<IntroScript>());
        Debug.Log("thisshitshouldbeadded");
        stateBasedScripts[stateBasedScripts.Count-1].StartState();
        EventManager.TriggerEvent("StartIntro");
        //game start animation and shit
    }

    void StartTutorial()
    {
        SetGameplayState(gameplaystate.running);
        stateBasedScripts.Add(gameObject.AddComponent<TutorialScript>());
        stateBasedScripts[stateBasedScripts.Count-1].StartState();
        EventManager.TriggerEvent("StartTutorial");
        //play tutorial
    }

    void StartBreakoutIntermission()
    {
        SetGameplayState(gameplaystate.paused);
        stateBasedScripts.Add(gameObject.AddComponent<BreakoutIntermissionScript>());
        stateBasedScripts[stateBasedScripts.Count-1].StartState();
        EventManager.TriggerEvent("BreakoutIntermission");
        //breakout start animation and shit
    }

    void StartBreakout()
    {
        CurrentLevel += 1;
        if (CurrentLevel == 2) BumperScript.AddBumpers();    //after defeating the boss once    TODO change when
        stateBasedScripts.Add(gameObject.AddComponent<BreakoutScript>());
        stateBasedScripts[stateBasedScripts.Count-1].StartState();
        SetGameplayState(gameplaystate.running);
        EventManager.TriggerEvent("StartBreakout");
        //start the main gameplay stuff
    }

    void StartBossIntermission()
    {
        SetGameplayState(gameplaystate.paused);
        stateBasedScripts.Add(gameObject.AddComponent<BossIntermissionScript>());
        stateBasedScripts[stateBasedScripts.Count-1].StartState();
        EventManager.TriggerEvent("BossIntermission");
        //boss start animation and shit
    }

    void StartBoss()
    {
        SetGameplayState(gameplaystate.running);
        stateBasedScripts.Add(gameObject.AddComponent<BossScript>());
        stateBasedScripts[stateBasedScripts.Count-1].StartState();
        EventManager.TriggerEvent("StartBoss");
        //actually start the bossfight logics
    }

    void GameOver()
    {
        SetGameplayState(gameplaystate.paused);
        stateBasedScripts.Add(gameObject.AddComponent<GameOverScript>());
        stateBasedScripts[stateBasedScripts.Count-1].StartState();
        EventManager.TriggerEvent("GameOver", null, points);
        IsQuitting = true;
        //game is over. destroy (explode?) everything
    }

    void Update()
    {
        updateUIValues();
    }

    public gamestate CurrentGamestate
    { get { return gameState; } }
    
    public gameplaystate CurrentGameplaystate
    { get { return gameplayState; } }

    public int CurrentEnergy
    { get { return energy; } }

    public float CurrentEnergyUI
    { get { return 1f/(float)maxenergy*(float)energy; } }

    public int CurrentPoints
    { get { return points; } }

    public int CurrentUIPoints
    { get { return (int)screenpoints; } }

    public int CurrentBarrelGoo
    { get { return Barrelcontent; } }

    public float CurrentBarrelGooUI
    { get { return 1f/(float)GooNeededForBoss*(float)Barrelcontent; } }

    public void EmptyBarrelGoo()
    {
        Barrelcontent = 0;
    }

    public void AddPoints(int p)
    {
        if (CurrentGamestate == gamestate.Tutorial) return;
        points += p;
        EventManager.TriggerEvent("GainedPoints", null, p);
        if (points >= 125 && points < 300) BumperScript.AddBumpers();   //TODO 'level'-system
    }

    public int gooNeededForBoss
    {
        get { return GooNeededForBoss; }
        set { GooNeededForBoss = value; }
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
        if (CurrentGamestate == gamestate.Tutorial) return;
        energy -= e;
        EventManager.TriggerEvent("LoseEnergy", null, e);
        if (energy <= 0)
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

    public void AddGooToBarrel(int goo)
    {
        Barrelcontent += goo;
    }

    void updateUIValues()
    {
        if (screenBarrelContent > Barrelcontent)
        {
            //decrease screenbarrelcontent
            if (screenBarrelContent - 1 < Barrelcontent)
                screenBarrelContent = Barrelcontent;
            else
            {

                screenBarrelContent -= 1 / Time.deltaTime;
            }

        }
        else if (screenBarrelContent < Barrelcontent)
        {
            //increase screenbarrelcontent
            if (screenBarrelContent + 1 > Barrelcontent)
                screenBarrelContent = Barrelcontent;
            else
            {

                screenBarrelContent += 1 / Time.deltaTime;
            }
        }

        if (screenpoints > points)
        {
            //decrease screenpoints
            if (screenpoints - 1 < points)
                screenpoints = points;
            else
            {

                screenpoints -= 1 / Time.deltaTime;
            }
        }
        if (screenpoints < points)
        {
            //increase screenpoints
            if (screenpoints + 1 > points)
                screenpoints = points;
            else
            {

                screenpoints += 1 / Time.deltaTime;
            }
        }

        //screenBarrelContent = (Barrelcontent - screenBarrelContent) / Mathf.Abs(Barrelcontent - screenBarrelContent);
        //screenpoints = (points - screenpoints) / Mathf.Abs(points - screenpoints);
    }
}
