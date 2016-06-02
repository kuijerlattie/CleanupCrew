using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

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
    /// call ONLY during EventManager init
    /// </summary>
    public static void Init()
    {
        gameManager = FindObjectOfType<GameManager>();
        powerupManager = FindObjectOfType<PowerupManager>();
        initialized = true;
    }

   

    /// <summary>
    /// used to get positions of all kinds of objects in the scene
    /// </summary>
    public class Positions
    {
        /// <summary>
        /// returns the position of the paddle, NOTE!!: if multiple paddles you might not get the correct one
        /// </summary>
        public static Vector3 paddle
        { get { return GameObject.FindGameObjectWithTag("paddle").transform.position; } }

        public static Vector3 Water
        { get { return GameObject.FindGameObjectWithTag("water").transform.position; } }

        public static Vector3 Space
        { get { return GameObject.FindGameObjectWithTag("space").transform.position; } }

        public static Vector3 Underground
        { get { return GameObject.FindGameObjectWithTag("sand").transform.position; } }
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
    /// returns the time since the phase has switched
    /// </summary>
    public static float elapsedTimethisPhase
    { get { return gameManager.elapsedTimeThisPhase; } }

    /// <summary>
    /// returns the time since the last userinput was registered
    /// </summary>
    public static float idleTime
    { get { return gameManager.idleTimer; } }

    /// <summary>
    /// returns the time since the last userinput was registered
    /// </summary>
    public static int idleTimeInt
    { get { return (int)gameManager.idleTimer; } }

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

    // <summary>
    /// returns the amount of blobs in the scene
    /// </summary>
    public static int blobcount
    { get { return GameObject.FindGameObjectsWithTag("Blob").GetLength(0); } }

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


    /// <summary>
    /// used to set all properties in an array at once
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="array"></param>
    /// <param name="paramname"></param>
    /// <param name="value"></param>
    public static void SetAll<T1, T2>(GameObject[] array, string paramname, T2 value)
        where T1 : class
        where T2 : struct
    {
        foreach(GameObject go in array)
        {
            T1 script = go.GetComponent<T1>();
            PropertyInfo property = script.GetType().GetProperty(paramname);
            property.SetValue(script, value, null);
        }
    }


    /// <summary>
    /// used to spawn a prefab that gets destroyed after x seconds
    /// not just messages, any prefab
    /// </summary>
    /// <param name="path"></param>
    /// <param name="time"></param>
    public static void DisplayMessage(string path, float time)
    {
        MessageScript.SpawnMessage(path, time);
    }


    /// <summary>
    /// used to spawn a prefab that gets destroyed through 'destroyMethod'
    /// not just messages, any prefab
    /// </summary>
    /// <param name="path"></param>
    /// <param name="destroyMethod"></param>
    public static void DisplayMessage(string path, Func<bool> destroyMethod)
    {
        MessageScript.SpawnMessage(path, destroyMethod);
    }


    /// <summary>
    /// assuming they are located in: Resources/Particles/ only specift name: Fire, Steam.. etc
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject SpawnParticle(string name, Vector3 position)
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Particles/" + name)) as GameObject;
        g.transform.position = position;
        return g;
    }

    public static GameObject SpawnParticle(string name, Vector3 position, Func<bool> destroyMethod)
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Particles/" + name)) as GameObject;
        MessageScript m = g.AddComponent<MessageScript>();
        m.DestroyMethod = destroyMethod;
        g.transform.position = position;
        return g;
    }

    /// <summary>
    /// assuming they are located in: Resources/Particles/ only specift name: Fire, Steam.. etc
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject SpawnParticle(string name, Transform ptransform)
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Particles/" + name)) as GameObject;
        g.transform.position = ptransform.position;
        return g;
    }

    public static GameObject SpawnParticle(string name, Transform ptransform, Func<bool> destroyMethod)
    {
        GameObject g = GameObject.Instantiate(Resources.Load("Particles/" + name)) as GameObject;
        MessageScript m = g.AddComponent<MessageScript>();
        m.DestroyMethod = destroyMethod;
        g.transform.position = ptransform.position;
        return g;
    }





    //public static Settings settings;  //------------------------------------------------

    public static class Settings
    {
        /// <summary>
        /// get/set the blob speed
        /// </summary>
        public static float blobSpeed
        { 
            get { return GameSettings.BlobSpeedS; }
            set { GameSettings.BlobSpeedS = value; }
        }
        
        public static float paddleDistance
        {
            get { return GameSettings.PaddleDistanceS; }
            set { GameSettings.PaddleDistanceS = value; }
        }

        public static float paddleRotation
        {
            get { return GameSettings.PaddleRotationS; }
            set { GameSettings.PaddleRotationS = value; }
        }

        public static float touchBarSize
        {
            get { return GameSettings.TouchBarSizeS; }
            set { GameSettings.TouchBarSizeS = value; }
        }

        public static int tutorialAmountOfRings
        {
            get { return GameSettings.AmountOfRingsS; }
            set { GameSettings.AmountOfRingsS = value; }
        }

        public static int cleanupSpawnBossAfter
        {
            get { return GameSettings.SpawnBossAfterS; }
            set { GameSettings.SpawnBossAfterS = value; }
        }

        public static float cleanupBallSpawnInterval
        {
            get { return GameSettings.ballSpawnIntervalS; }
            set { GameSettings.ballSpawnIntervalS = value; }
        }

        public static float cleanupSpawnIntervalIncrease
        {
            get { return GameSettings.spawnIntervalIncreaseS; }
            set { GameSettings.spawnIntervalIncreaseS = value; }
        }

        //this variable is currently not used during the spawning
        public static float cleanupSpawnIntervalPowerIncrease
        {
            get;
            //{ return GameSettings.spawnIntervalPowerIncreaseS; }
            set;// { GameSettings.spawnIntervalPowerIncreaseS = value; }
        }

        public static float bossProjectileFireSpeed
        {
            get { return GameSettings.ProjectileFireSpeedS; }
            set { GameSettings.ProjectileFireSpeedS = value; }
        }

        public static float bossProjectileSpeedMultiplier
        {
            get { return GameSettings.ProjectileSpeedMultiplierS; }
            set { GameSettings.ProjectileSpeedMultiplierS = value; }
        }

        public static float bossDamagePerHitTaken
        {
            get { return GameSettings.DamagePerHitTakenS; }
            set { GameSettings.DamagePerHitTakenS = value; }
        }

       



    }
}
