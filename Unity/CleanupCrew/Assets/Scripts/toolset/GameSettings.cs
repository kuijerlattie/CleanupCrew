using UnityEngine;
using System.Collections;


//all statics have the same name with 'S' at the end
//all privates are there only for the inspector;
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
    static public float BlobSpeedS = 9.0f;



    [Header("Paddle")]      //-----------------------------------------------------------------------------------------

    [SerializeField, Range(10.0f, 30.0f)]
    private float PaddleDistance = PaddleDistanceS;
    static public float PaddleDistanceS = 18.0f;

    //if we switch to tapping on the actual circle again instead of the bar on the bottom this is NOT used
    [SerializeField, Range(180.0f, 540.0f), Tooltip("degrees the paddle rotates when swiping from most left to most right")]
    private float PaddleRotation = PaddleRotationS;
    static public float PaddleRotationS = 360.0f;

    [Space(10)] //used to space out in inspector

    [SerializeField, Range(1.0f, 100.0f), Tooltip("percent of the screen (on the bottom) that the slider bar is")]
    private float TouchBarSize = TouchBarSizeS;
    static public float TouchBarSizeS = 25.0f;




    [Header("Tutorial")]    //-----------------------------------------------------------------------------------------
    
    [SerializeField, Range(3, 5)]
    private int AmountOfRings = AmountOfRingsS;
    static public int AmountOfRingsS = 3;



    [Header("Cleanup")]     //-----------------------------------------------------------------------------------------

    [SerializeField, Range(1, 20)]
    private int SpawnBossAfter = SpawnBossAfterS;
    static public int SpawnBossAfterS = 2;   //goes to battle phase after x amount of blobs went into the same zone (space/water/underground)

    [SerializeField, Range(0.0f, 10.0f)]
    private float ballSpawnInterval = ballSpawnIntervalS;
    static public float ballSpawnIntervalS = 10;

    [SerializeField, Range(0.0f,1.0f)]
    private float spawnIntervalIncrease = spawnIntervalIncreaseS;
    static public float spawnIntervalIncreaseS = 0.9f;

    [SerializeField, Range(0.0f, 1.0f)]
    private float spawnIntervalPowerIncrease = spawnIntervalPowerIncreaseS;
    static public float spawnIntervalPowerIncreaseS = 0.1f;



    [Header("Battle/Bosses")]   //-----------------------------------------------------------------------------------------

    [SerializeField, Range(1, 10)]
    private float ProjectileFireSpeed = ProjectileFireSpeedS;
    static public float ProjectileFireSpeedS = 3.0f;  //shoots every x seconds

    [SerializeField, Range(0.1f, 3.0f)]
    private float ProjectileSpeedMultiplier = ProjectileSpeedMultiplierS;
    static public float ProjectileSpeedMultiplierS = 1.0f;   //x times the speed of regular blobs

    [SerializeField, Range(0.0f, 100.0f)]
    private float DamagePerHitTaken = DamagePerHitTakenS;
    static public float DamagePerHitTakenS = 10.0f;  //enemies take x percent damage of their full hp



    [Header("Powerups")]   //-----------------------------------------------------------------------------------------

    [SerializeField]
    private bool PowerupsInTutorial = PowerupsInTutorialS;    //SHOULD be false, can mess up the rods currently
    static public bool PowerupsInTutorialS = false;

    [SerializeField]
    private bool PowerupsInCleanup = PowerupsInCleanupS;    //SHOULD be true
    static public bool PowerupsInCleanupS = true;

    [SerializeField]
    private bool PowerupsInBattle = PowerupsInBattleS;    //SHOULD be true
    static public bool PowerupsInBattleS = true;

    [Space(10)] //used to space out in inspector

    [SerializeField]
    private bool PowSpawnInCenter = PowSpawnInCenterS;  
    static public bool PowSpawnInCenterS = true;

    [SerializeField, Range(1.0f, 50.0f), Tooltip("Seconds")]
    private float PowSpawnRate = PowSpawnRateS;
    static public float PowSpawnRateS = 25.0f;

    [SerializeField, Range(1.0f, 50.0f), Tooltip("random difference changing original spawnrate with x")]
    private float PowSpawnRateDiff = PowSpawnRateDiffS;
    static public float PowSpawnRateDiffS = 25.0f;

    [Space(10)] //used to space out in inspector

    [SerializeField, Range(1.0f, 99.0f), Tooltip("Seconds")]
    private float PowDuration = PowDurationS;
    static public float PowDurationS = 25.0f;





    void Reset()    //TODO regular reset in inspector doesnt work because ¿¿¿¿¿statics??????
    {
        
        //AmountOfRings = AmountOfRingsDEFAULT;   //TODO add default variables for each variable... lot of work....
    }



    //used to set all the values to their inspector values, is automatically called
    //if any of the above variables is not in here it does not work through the inspector
    void OnValidate()
    {
        //general
        BlobSpeedS = BlobSpeed;

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

    }
}
