
﻿using UnityEngine;
using System.Collections;


//all statics have the same name with 'S' at the end, these are accesible from other scripts
//all privates are there only for the inspector;
//all defaults are full capitalized

//TODO maybe add [Tooltip("put description of each variable here")] , this is seen when mousing over a variable in the inspector
//TODO range values are chosen pretty random
//because 'OnValidate' this script does NOT have to be first in the hierarchy
//TODO make every variable into 'Getter' only because these values should ONLY be changes through the inspector
//     , when doing this make a seperate variable otherwise it also cannot be changed from the inspector. EDIT: { get; private set; }

public class GameSettings : MonoBehaviour
{
    [Header("General")]     //-----------------------------------------------------------------------------------------

    [SerializeField, Range(5.0f, 20.0f)]
    private float BlobSpeed = BlobSpeedS;
    static public float BlobSpeedS = BLOBSPEED;
    private const float BLOBSPEED = 9.0f;

    [SerializeField]
    private float BlobScale = BlobScaleS;
    static public float BlobScaleS = BLOBSCALE;
    private const float BLOBSCALE = 1.0f;

    [SerializeField]
    private float CircleScale = CircleScaleS;
    static public float CircleScaleS = CIRCLESCALE;
    private const float CIRCLESCALE = 1.0f;


    [Header("Paddle")]      //-----------------------------------------------------------------------------------------

    [SerializeField, Range(10.0f, 30.0f), Tooltip("CURRENT BUG: does not work if multiple paddle powerups is also activated from start")]
    private float PaddleDistance = PaddleDistanceS;
    static public float PaddleDistanceS = PADDLEDISTANCE;
    private const float PADDLEDISTANCE = 18.0f;

    //if we switch to tapping on the actual circle again instead of the bar on the bottom this is NOT used
    [SerializeField, Range(180.0f, 540.0f), Tooltip("degrees the paddle rotates when swiping from most left to most right")]
    private float PaddleRotation = PaddleRotationS;
    static public float PaddleRotationS = PADDLEROTATION;
    private const float PADDLEROTATION = 360.0f;

    [Space(10)] //used to space out in inspector

    [SerializeField, Range(1.0f, 100.0f), Tooltip("percent of the screen (on the bottom) that the slider bar is")]
    private float TouchBarSize = TouchBarSizeS;
    static public float TouchBarSizeS = TOUCHBARSIZE;
    private const float TOUCHBARSIZE = 25.0f;




    [Header("Tutorial")]    //-----------------------------------------------------------------------------------------

    [SerializeField, Range(3, 5)]
    private int AmountOfRings = AmountOfRingsS;
    static public int AmountOfRingsS = AMOUNTOFRINGS;
    private const int AMOUNTOFRINGS = 3;



    [Header("Cleanup")]     //-----------------------------------------------------------------------------------------

    [SerializeField, Range(1, 20)]
    private int SpawnBossAfter = SpawnBossAfterS;
    static public int SpawnBossAfterS = SPAWNBOSSAFTER;   //goes to battle phase after x amount of blobs went into the same zone (space/water/underground)
    private const int SPAWNBOSSAFTER = 2;

    [SerializeField, Range(0.0f, 10.0f)]
    private float ballSpawnInterval = ballSpawnIntervalS;
    static public float ballSpawnIntervalS = BALLSPAWNINTERVAL;
    private const float BALLSPAWNINTERVAL = 10.0f;

    [SerializeField, Range(0.0f, 1.0f)]
    private float spawnIntervalIncrease = spawnIntervalIncreaseS;
    static public float spawnIntervalIncreaseS = SPAWNINTERVALINCREASE;
    private const float SPAWNINTERVALINCREASE = 0.9f;

    [SerializeField, Range(0.0f, 1.0f)]
    private float spawnIntervalPowerIncrease = spawnIntervalPowerIncreaseS;
    static public float spawnIntervalPowerIncreaseS = SPAWNINTERVALPOWERINCREASE;
    private const float SPAWNINTERVALPOWERINCREASE = 0.1f;



    [Header("Battle/Bosses")]   //-----------------------------------------------------------------------------------------

    [SerializeField, Range(1, 10), Tooltip("shoots every x seconds")]
    private float ProjectileFireSpeed = ProjectileFireSpeedS;
    static public float ProjectileFireSpeedS = PROJECTILEFIRESPEED;  //shoots every x seconds
    private const float PROJECTILEFIRESPEED = 3.0f;

    [SerializeField, Range(0.1f, 3.0f), Tooltip("x times the speed of regular blobs")]
    private float ProjectileSpeedMultiplier = ProjectileSpeedMultiplierS;
    static public float ProjectileSpeedMultiplierS = PROJECTILESPEEDMULTIPLIER;   //x times the speed of regular blobs
    private const float PROJECTILESPEEDMULTIPLIER = 1.0f;

    [SerializeField, Range(0.0f, 100.0f), Tooltip("percent of total health")]
    private float DamagePerHitTaken = DamagePerHitTakenS;
    static public float DamagePerHitTakenS = DAMAGEPERHITTAKEN;  //enemies take x percent damage of their full hp
    private const float DAMAGEPERHITTAKEN = 10.0f;



    [Header("Powerups")]   //-----------------------------------------------------------------------------------------

    [SerializeField]
    private bool PowerupsInTutorial = PowerupsInTutorialS;    //SHOULD be false, can mess up the rods currently
    static public bool PowerupsInTutorialS = POWERUPSINTUTORIAL;
    private const bool POWERUPSINTUTORIAL = false;

    [SerializeField]
    private bool PowerupsInCleanup = PowerupsInCleanupS;    //SHOULD be true
    static public bool PowerupsInCleanupS = POWERUPSINCLEANUP;
    private const bool POWERUPSINCLEANUP = true;

    [SerializeField]
    private bool PowerupsInBattle = PowerupsInBattleS;    //SHOULD be true
    static public bool PowerupsInBattleS = POWERUPSINBATTLE;
    private const bool POWERUPSINBATTLE = true;

    [Space(10)] //used to space out in inspector

    [SerializeField, Tooltip("if false it spawn from idk, the zones? (water, space, underground)")]
    private bool PowSpawnInCenter = PowSpawnInCenterS;
    static public bool PowSpawnInCenterS = POWSPAWNINCENTER;
    private const bool POWSPAWNINCENTER = true;

    [SerializeField, Range(1.0f, 50.0f), Tooltip("Seconds")]
    private float PowSpawnRate = PowSpawnRateS;
    static public float PowSpawnRateS = POWSPAWNRATE;
    private const float POWSPAWNRATE = 25.0f;

    [SerializeField, Range(1.0f, 50.0f), Tooltip("random difference changing original spawnrate with x")]
    private float PowSpawnRateDiff = PowSpawnRateDiffS;
    static public float PowSpawnRateDiffS = POWSPAWNRATEDIFF;
    private const float POWSPAWNRATEDIFF = 10.0f;

    [Space(10)] //used to space out in inspector

    [SerializeField, Range(1.0f, 99.0f), Tooltip("Seconds")]
    private float PowDuration = PowDurationS;
    static public float PowDurationS = POWDURATION;
    private const float POWDURATION = 10.0f;



    [Header("Powerups TESTING")]    //----------------------------------------------------------------------

    [SerializeField]
    private int AmountOfPaddles = AmountOfPaddlesS;
    static public int AmountOfPaddlesS = AMOUNTOFPADDLES;
    private const int AMOUNTOFPADDLES = 1;

    [SerializeField]
    private bool Magnetic = MagneticS;
    static public bool MagneticS = MAGNETIC;
    private const bool MAGNETIC = false;

    [SerializeField]
    private bool Slow = SlowS;
    static public bool SlowS = SLOW;
    private const bool SLOW = false;

    [SerializeField]
    private bool SmallEnemies = SmallEnemiesS;
    static public bool SmallEnemiesS = SMALLENEMIES;
    private const bool SMALLENEMIES = false;

    [SerializeField]
    private bool BigPaddle = BigPaddleS;
    static public bool BigPaddleS = BIGPADDLE;
    private const bool BIGPADDLE = false;

    [SerializeField]
    private bool SmallPaddle = SmallPaddleS;
    static public bool SmallPaddleS = SMALLPADDLE;
    private const bool SMALLPADDLE = false;





    void Reset()
    {
        //general
        BlobSpeed = BLOBSPEED;
        BlobScale = BLOBSCALE;
        CircleScale = CIRCLESCALE;

        //paddle
        PaddleDistance = PADDLEDISTANCE;
        PaddleRotation = PADDLEROTATION;
        TouchBarSize = TOUCHBARSIZE;

        //tutorial
        AmountOfRings = AMOUNTOFRINGS;

        //cleanup
        SpawnBossAfter = SPAWNBOSSAFTER;
        ballSpawnInterval = BALLSPAWNINTERVAL;
        spawnIntervalIncrease = SPAWNINTERVALINCREASE;
        spawnIntervalPowerIncrease = SPAWNINTERVALPOWERINCREASE;

        //battle
        ProjectileFireSpeed = PROJECTILEFIRESPEED;
        ProjectileSpeedMultiplier = PROJECTILESPEEDMULTIPLIER;
        DamagePerHitTaken = DAMAGEPERHITTAKEN;

        //powerups
        PowerupsInTutorial = POWERUPSINTUTORIAL;
        PowerupsInCleanup = POWERUPSINCLEANUP;
        PowerupsInBattle = POWERUPSINBATTLE;

        PowSpawnInCenter = POWSPAWNINCENTER;
        PowSpawnRate = POWSPAWNRATE;
        PowSpawnRateDiff = POWSPAWNRATEDIFF;

        PowDuration = POWDURATION;


        //powerup TESTING
        AmountOfPaddles = AMOUNTOFPADDLES;
        Magnetic = MAGNETIC;
        Slow = SLOW;
        SmallEnemies = SMALLENEMIES;
        BigPaddle = BIGPADDLE;
        SmallPaddle = SMALLPADDLE;
    }



    //used to set all the values to their inspector values, is automatically called
    //if any of the above variables is not in here it does not work through the inspector
    void OnValidate()
    {
        //general
        BlobSpeedS = BlobSpeed;
        BlobScaleS = BlobScale;
        CircleScaleS = CircleScale;

        //paddle
        PaddleDistanceS = PaddleDistance;
        PaddleRotationS = PaddleRotation;
        TouchBarSizeS = TouchBarSize;

        //tutorial
        AmountOfRingsS = AmountOfRings;

        //cleanup
        SpawnBossAfterS = SpawnBossAfter;
        ballSpawnIntervalS = ballSpawnInterval;
        spawnIntervalIncreaseS = spawnIntervalIncrease;
        spawnIntervalPowerIncreaseS = spawnIntervalPowerIncrease;

        //battle
        ProjectileFireSpeedS = ProjectileFireSpeed;
        ProjectileSpeedMultiplierS = ProjectileSpeedMultiplier;
        DamagePerHitTakenS = DamagePerHitTaken;

        //powerups
        PowerupsInTutorialS = PowerupsInTutorial;
        PowerupsInCleanupS = PowerupsInCleanup;
        PowerupsInBattleS = PowerupsInBattle;

        PowSpawnInCenterS = PowSpawnInCenter;
        PowSpawnRateS = PowSpawnRate;
        PowSpawnRateDiffS = PowSpawnRateDiff;

        PowDurationS = PowDuration;

        //powerups TESTING
        AmountOfPaddlesS = AmountOfPaddles;
        MagneticS = Magnetic;
        SlowS = Slow;
        SmallEnemiesS = SmallEnemies;
        BigPaddleS = BigPaddle;
        SmallPaddleS = SmallPaddle;

    }
}