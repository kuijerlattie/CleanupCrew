using UnityEngine;
using System.Collections.Generic;

public abstract class AbstractPhase : MonoBehaviour {

    protected bool isActive = false;    //used to Pause the game
    public abstract void StartPhase();
    public abstract void StopPhase();
    public abstract bool HasEnded();
    public GameManager.gamestate nextGamestate;

    protected List<GameObject> pointZones = new List<GameObject>();
    /// <summary>
    /// enables/disables the pointzones
    /// </summary>
    /// <param name="isTutorial"></param>
    /// 
    protected void FindPointZones()
    {
        PointScript[] pointScripts = GameObject.FindObjectsOfType<PointScript>();
        foreach (PointScript point in pointScripts)
        {
            pointZones.Add(point.gameObject);
        }
    }

    protected void SetPointZones(bool isTutorial)
    {
        if (!isTutorial)
        {
            foreach (GameObject pointzone in pointZones)
            {
                pointzone.SetActive(true);
            }
            return;
        }
        foreach (GameObject point in pointZones)
        {
            point.SetActive(!isTutorial);
        }
    }

}
