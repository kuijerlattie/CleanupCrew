using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

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
        switch (state)
        {
            case gamestate.Start:
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

    void StartIntro()
    {
        GameObject.Instantiate(Resources.Load("prefabs/paddle"));
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

    public void AddPoints(int p)
    {
        points += p;
        EventManager.TriggerEvent("GainedPoints", null, p);
    }

}
