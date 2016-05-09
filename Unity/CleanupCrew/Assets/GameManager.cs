using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    const float GENERATIONTIME = 5; //3 minutes
    const float CLEANINGTIME = 60;
    float gameTimer = 15;
    bool timerPaused = false;

    public int points;
    public int power;

    [SerializeField]
    Text pointText;
    [SerializeField]
    Text powerText;

    public enum gamestate
    {
        Generating,
        Cleaning
    }

    gamestate state;

    GameObject g;

    [SerializeField] Text timertext;
    [SerializeField]
    GameObject breakoutObjects;


    public void ResetTimer(bool generationStage = true)
    {
        if (generationStage)
        {
            timerPaused = false;
            gameTimer = GENERATIONTIME;
        }
        else
        {
            timerPaused = false;
            state = gamestate.Cleaning;
            gameTimer = CLEANINGTIME;
        }
    }

    public void PauseTimer()
    {
        timerPaused = true;
    }
	// Use this for initialization
	void Start () {
        state = gamestate.Generating;
        ResetTimer();
        PauseTimer();

        g = new GameObject("ShapeContainer");
	}
	
	// Update is called once per frame
	void Update () {
        timertext.text = "Time left: " + gameTimer;
        if (gameTimer > 0 && !timerPaused)
        {
            gameTimer -= Time.deltaTime;
            if (state == gamestate.Cleaning)
            {
                if (g.transform.childCount <= 0)
                {
                    timertext.text = "Je hebt gewonnen met een score van 0!";
                }
            }
        }
        else
        {
            if (gameTimer <= 0)
            {
                ResetTimer(false);  //set timer to 'breakout' timelimit

                //remove all balls that are not exploded yet
                Explosion[] allBalls = GameObject.FindObjectsOfType<Explosion>();
                for (int i = 0; i < allBalls.GetLength(0); i++)
                {
                    if (!allBalls[i].isActiveAndEnabled)
                        GameObject.Destroy(allBalls[i].gameObject);
                    else
                    {
                        allBalls[i].GetComponent<SphereCollider>().isTrigger = false;   //mayble this is duplicate now
                        allBalls[i].transform.SetParent(g.transform);
                        allBalls[i].gameObject.AddComponent<BreakoutBlock>();
                        GameObject.Destroy(allBalls[i].GetComponent<Rigidbody>());
                    }
                }

                //for (int i = Explosion.explodedSpheres.Count-1; i > 0; i--)
                //{
                //    GameObject newObject = GameObject.Instantiate(Explosion.explodedSpheres[i].gameObject);
                //    newObject.transform.Translate(new Vector3(25, 0, 0));
                //}

                breakoutObjects.SetActive(true);    //enable paddle etc to start 'breakout' stage

                //TODO Game end
            }
        }
	}
}
