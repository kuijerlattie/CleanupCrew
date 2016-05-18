using UnityEngine;
using System.Collections;

public class SI : MonoBehaviour {

    //non-statics
    private static GameManager gameManager;
    private static PowerupManager powerupManager;

    //statics

    //for this file
    private static bool initialized = false;
    
    //helper variables
    public static Vector3 gameCenter = new Vector3(0, 0, 0);

    /// <summary>
    /// initialize the Scripting interface
    /// </summary>
    public static void Init()
    {
        gameManager = FindObjectOfType<GameManager>();
        powerupManager = FindObjectOfType<PowerupManager>();
        initialized = true;
    }

    /// <summary>
    /// returns the amount of points the player has earned
    /// </summary>
    public static int points
    { get { return gameManager.points; } }

    /// <summary>
    /// returns the amount of points the player has earned in the water area
    /// </summary>
    public static int pointsWater
    { get { return gameManager.pointsWater; } }

    /// <summary>
    /// returns the amount of points the player has earned in the underground area
    /// </summary>
    public static int pointsUnderground
    { get { return gameManager.pointsUnderground; } }

    /// <summary>
    /// returns the amount of points the player has earned in the space area
    /// </summary>
    public static int pointsSpace
    { get { return gameManager.pointsSpace; } }

    /// <summary>
    /// returns the power
    /// </summary>
    public static float power
    { get { return gameManager.power; } }

    /// <summary>
    /// returns the time since the game started playing
    /// </summary>
    public static float elapsedTime
    { get { return gameManager.elapsedTime; } }

    /// <summary>
    /// returns the time since the last userinput was registered
    /// </summary>
    public static float idleTime
    { get { return gameManager.idleTimer; } }

    /// <summary>
    /// returns the amount of paddles in the game;
    /// </summary>
    public static int paddleCount
    { get { return gameManager.paddles.Count; } }

    /// <summary>
    /// get or set the gamestate. Setting it will stop the current gamestate
    /// </summary>
    public static GameManager.gamestate gameState
    { 
        get { return gameManager.currentState; }
        set { gameManager.SetState(value); }
    }

    /// <summary>
    /// returns the amount of balls in the scene
    /// </summary>
    public static int ballcount
    { get { return GameObject.FindGameObjectsWithTag("Ball").GetLength(0); } }

    /// <summary>
    /// spawn a ball
    /// </summary>
    /// <param name="position">location to spawn the ball</param>
    /// <returns>the ball that was spawned</returns>
    public static GameObject SpawnBall(Vector3 position)
    {
        return SpawnSpheres.SpawnSphere(position);
    }

    /// <summary>
    /// spawn a ball
    /// </summary>
    /// <param name="position">location to spawn the ball</param>
    /// <param name="direction">direction to make the ball move towards</param>
    /// <returns>the ball that was spawned</returns>
    public static GameObject SpawnBall(Vector3 position, Vector3 direction)
    {
        return SpawnSpheres.SpawnSphere(position, direction);
    }

    /// <summary>
    /// spawn a ball
    /// </summary>
    /// <param name="position">location to spawn the ball</param>
    /// <param name="angle">the angle to make the ball move towards</param>
    /// <returns>the ball that was spawned</returns>
    public static GameObject SpawnBall(Vector3 position, int angle)
    {
        return SpawnSpheres.SpawnSphere(position, Quaternion.AngleAxis(angle, Vector3.up) * new Vector3(0, 0, 1));
    }

    /// <summary>
    /// disables all powerups
    /// </summary>
    public static void StopAllPowerups()
    {
        powerupManager.EndAllPowerups();
    }
    
    /// <summary>
    /// spawns a random powerup
    /// </summary>
    public static void SpawnPowerup()
    {
        powerupManager.SpawnRandomPowerup();
    }

    /// <summary>
    /// spawns the selected powerup
    /// </summary>
    /// <param name="type">the powerup to spawn</param>
    public static void SpawnPowerup(PowerupManager.PowerupType type)
    {
        powerupManager.ActivatePowerup(type);
    }


}
