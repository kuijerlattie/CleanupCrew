using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    const float GENERATIONTIME = 5; //3 minutes
    const float CLEANINGTIME = 60;
    float gameTimer = 15;
    bool timerPaused = false;

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
            gameTimer = CLEANINGTIME;
        }
    }

    public void PauseTimer()
    {
        timerPaused = true;
    }
	// Use this for initialization
	void Start () {
        ResetTimer();
        PauseTimer();
	}
	
	// Update is called once per frame
	void Update () {
        timertext.text = "Time left: " + gameTimer;
        if (gameTimer > 0 && !timerPaused)
            gameTimer -= Time.deltaTime;
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
                    else {
                        allBalls[i].GetComponent<SphereCollider>().isTrigger = false;   //mayble this is duplicate now
                    }
                }

                for (int i = Explosion.explodedSpheres.Count-1; i > 0; i--)
                {
                    GameObject newObject = GameObject.Instantiate(Explosion.explodedSpheres[i].gameObject);
                    newObject.transform.Translate(new Vector3(25, 0, 0));
                }

                breakoutObjects.SetActive(true);    //enable paddle etc to start 'breakout' stage

                //TODO Game end
            }
        }
	}
}
