
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

    [System.Serializable]   //cant remember why its a serializable but leave it i guess
public class GameSettings : MonoBehaviour
{
    [Header("General")]     //-----------------------------------------------------------------------------------------

    [SerializeField, Range(5.0f, 20.0f)]
    private float BlobSpeed = BlobSpeedS;
    static public float BlobSpeedS = BLOBSPEED;
    private const float BLOBSPEED = 10.0f;

    [SerializeField, Range(0.1f, 5.0f)]
    private float BlobScale = BlobScaleS;
    static public float BlobScaleS = BLOBSCALE;
    private const float BLOBSCALE = 2.0f;

    [SerializeField, Range(3, 50)]
    private int MaxBlobs = MaxBlobsS;
    static public int MaxBlobsS = MAXBLOBS;
    private const int MAXBLOBS = 15;

    [Space(10)] //used to space out in inspector

    [SerializeField, Range(0.1f, 3.0f), Tooltip("size of the big circle/ the level")]
    private float CircleScale = CircleScaleS;
    static public float CircleScaleS = CIRCLESCALE;
    private const float CIRCLESCALE = 1.5f;

    [SerializeField, Range(0.01f, 10.0f), Tooltip("the amount of energy you lose when blob hits wall")]
    private float WallPowerDamage = WallPowerDamageS;
    static public float WallPowerDamageS = WALLPOWERDAMAGE;
    private const float WALLPOWERDAMAGE = 3.0f;

    [SerializeField,Tooltip("true: blobs bounce off walls.  false: blobs get destroyed by walls")]
    private bool BounceWall = BounceWallS;
    static public bool BounceWallS = BOUNCEWALL;
    private const bool BOUNCEWALL = true;








    [Header("Paddle")]      //-----------------------------------------------------------------------------------------

    [SerializeField, Range(10.0f, 30.0f), Tooltip("distance from the middle")]
    private float PaddleDistance = PaddleDistanceS;
    static public float PaddleDistanceS = PADDLEDISTANCE;
    private const float PADDLEDISTANCE = 25.0f;

    //if we switch to tapping on the actual circle again instead of the bar on the bottom this is NOT used
    [SerializeField, Range(180.0f, 540.0f), Tooltip("degrees the paddle rotates when swiping from most left to most right")]
    private float PaddleRotation = PaddleRotationS;
    static public float PaddleRotationS = PADDLEROTATION;
    private const float PADDLEROTATION = 360.0f;

    [SerializeField, Range(1.0f, 15.0f), Tooltip("requires: 'PaddleBounce'.  how much the paddle bounces the blobs based on the distance to center of the paddle")]
    private float PaddleBounceStrength = PaddleBounceStrengthS;
    static public float PaddleBounceStrengthS = PADDLEBOUNCESTRENGTH;
    private const float PADDLEBOUNCESTRENGTH = 5.0f;

    [SerializeField, Tooltip("old controls: tap where to go,    new controls: slider at bottom of screen")]
    private bool OldControls = OldControlsS;
    static public bool OldControlsS = OLDCONTROLS;
    private const bool OLDCONTROLS = true;

    [SerializeField, Range(0, 15),Tooltip("Max amount of Blobs/balls that can be held by the paddle")]
    private int MaxPaddleObjects = MaxPaddleObjectsS;
    static public int MaxPaddleObjectsS = MAXPADDLEOBJECTS;
    private const int MAXPADDLEOBJECTS = 3;

    [Space(10)] //used to space out in inspector

    [SerializeField, Range(1.0f, 100.0f), Tooltip("requires: 'oldcontrols = false'.     percent of the screen (on the bottom) that the slider bar is")]
    private float TouchBarSize = TouchBarSizeS;
    static public float TouchBarSizeS = TOUCHBARSIZE;
    private const float TOUCHBARSIZE = 25.0f;




    [Header("Tutorial")]    //-----------------------------------------------------------------------------------------

    [SerializeField, Range(3, 5)]
    private int AmountOfRings = AmountOfRingsS;
    static public int AmountOfRingsS = AMOUNTOFRINGS;
    private const int AMOUNTOFRINGS = 3;

    [SerializeField, Range(0, 5)]
    private int CenterSizeInRings = CenterSizeInRingsS;
    static public int CenterSizeInRingsS = CENTERSIZEINRINGS;
    private const int CENTERSIZEINRINGS = 0;



    [Header("Cleanup")]     //-----------------------------------------------------------------------------------------

    [SerializeField, Range(1, 20), Tooltip("after x amount of blobs in 1 area, the boss battle begins")]
    private int SpawnBossAfter = SpawnBossAfterS;
    static public int SpawnBossAfterS = SPAWNBOSSAFTER;   //goes to battle phase after x amount of blobs went into the same zone (space/water/underground)
    private const int SPAWNBOSSAFTER = 5;

    [SerializeField, Range(0.0f, 10.0f)]
    private float ballSpawnInterval = ballSpawnIntervalS;
    static public float ballSpawnIntervalS = BALLSPAWNINTERVAL;
    private const float BALLSPAWNINTERVAL = 7.0f;

    [SerializeField, Range(0.0f, 1.0f)]
    private float spawnIntervalIncrease = spawnIntervalIncreaseS;
    static public float spawnIntervalIncreaseS = SPAWNINTERVALINCREASE;
    private const float SPAWNINTERVALINCREASE = 0.9f;





    [Header("Battle/Bosses")]   //-----------------------------------------------------------------------------------------

    [SerializeField, Range(1, 10), Tooltip("shoots every x seconds")]
    private float ProjectileFireSpeed = ProjectileFireSpeedS;
    static public float ProjectileFireSpeedS = PROJECTILEFIRESPEED;  //shoots every x seconds
    private const float PROJECTILEFIRESPEED = 3.0f;

    [SerializeField, Range(0.1f, 3.0f), Tooltip("x times the speed of regular blobs")]
    private float ProjectileSpeedMultiplier = ProjectileSpeedMultiplierS;
    static public float ProjectileSpeedMultiplierS = PROJECTILESPEEDMULTIPLIER;   //x times the speed of regular blobs
    private const float PROJECTILESPEEDMULTIPLIER = 1.25f;

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
    private const float POWSPAWNRATE = 10.0f;

    [SerializeField, Range(1.0f, 50.0f), Tooltip("random difference changing original spawnrate with x")]
    private float PowSpawnRateDiff = PowSpawnRateDiffS;
    static public float PowSpawnRateDiffS = POWSPAWNRATEDIFF;
    private const float POWSPAWNRATEDIFF = 5.0f;

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

    /// <summary>
    /// to make sure that when switching scenes the values change with it.. see 'ApplySettings' below
    /// </summary>
    void Awake()
    {
        ApplySettings();
    }
    /// <summary>
    /// only need to be called when switching scenes through script, because it doesnt use the inspector values then, for some reason?
    /// should not have to call this anywhere else, but if the settings are not saving for whatever reason there is this applysettings
    /// </summary>
    private void ApplySettings()
    {
        OnValidate();
    }

    void Reset()
    {
        //general
        BlobSpeed = BLOBSPEED;
        BlobScale = BLOBSCALE;
        MaxBlobs = MAXBLOBS;
        CircleScale = CIRCLESCALE;
        WallPowerDamage = WALLPOWERDAMAGE;
        BounceWall = BOUNCEWALL;

        //paddle
        PaddleDistance = PADDLEDISTANCE;
        PaddleRotation = PADDLEROTATION;
        PaddleBounceStrength = PADDLEBOUNCESTRENGTH;
        MaxPaddleObjects = MAXPADDLEOBJECTS;
        TouchBarSize = TOUCHBARSIZE;
        OldControls = OLDCONTROLS;

        //tutorial
        AmountOfRings = AMOUNTOFRINGS;

        //cleanup
        SpawnBossAfter = SPAWNBOSSAFTER;
        ballSpawnInterval = BALLSPAWNINTERVAL;
        spawnIntervalIncrease = SPAWNINTERVALINCREASE;
 

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
        MaxBlobsS = MaxBlobs;
        CircleScaleS = CircleScale;
        WallPowerDamageS = WallPowerDamage;
        BounceWallS = BounceWall;

        //paddle
        PaddleDistanceS = PaddleDistance;
        PaddleRotationS = PaddleRotation;
        PaddleBounceStrengthS = PaddleBounceStrength;
        MaxPaddleObjectsS = MaxPaddleObjects;
        TouchBarSizeS = TouchBarSize;
        OldControlsS = OldControls;

        //tutorial
        AmountOfRingsS = AmountOfRings;
        CenterSizeInRingsS = CenterSizeInRings;

        //cleanup
        SpawnBossAfterS = SpawnBossAfter;
        ballSpawnIntervalS = ballSpawnInterval;
        spawnIntervalIncreaseS = spawnIntervalIncrease;


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