using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour {
    
    Text tPoints;
    Text tEnergy;
    Text tWaterpoints;
    Text tEarthpoints;
    Text tSpacepoints;
    Text tState;
    Text tGameplayState;

    Image EnergyGauge;
    Image BarrelGauge;

    // Use this for initialization
    void Start () {
        tPoints = GameObject.Find("Points").GetComponent<Text>();
        tEnergy = GameObject.Find("Energy").GetComponent<Text>();
        tWaterpoints = GameObject.Find("Waterpoints").GetComponent<Text>();
        tEarthpoints = GameObject.Find("Sandpoints").GetComponent<Text>();
        tSpacepoints = GameObject.Find("Spacepoints").GetComponent<Text>();
        tState = GameObject.Find("GameState").GetComponent<Text>();
        tGameplayState = GameObject.Find("GameplayState").GetComponent<Text>();
        EnergyGauge = GameObject.Find("EnergyGauge").GetComponent<Image>();
        BarrelGauge = GameObject.Find("BarrelGauge").GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        tPoints.text = "points: " + GameManager.instance.CurrentPoints;
        tEnergy.text = "energy: " + GameManager.instance.CurrentEnergy;
        tState.text = "Current state: " + GameManager.instance.CurrentGamestate;
        tGameplayState.text = "current gameplay state: " + GameManager.instance.CurrentGameplaystate;
        EnergyGauge.fillAmount = GameManager.instance.CurrentEnergyUI;
        BarrelGauge.fillAmount = GameManager.instance.CurrentBarrelGooUI;

        if (GameManager.instance.CurrentGamestate == GameManager.gamestate.Boss)
        {
            tEarthpoints.text = "Boss health: " + GameObject.FindObjectOfType<MoleScript>().hitpoints;
            tSpacepoints.text = "Boss state: " + GameObject.FindObjectOfType<MoleScript>().state;
            tWaterpoints.text = "Boss invincible: " + FindObjectOfType<MoleScript>().invincible;
        }
        else
        {
            tEarthpoints.text = "No Boss";
            tWaterpoints.text = "Goo in barrel: " + GameManager.instance.CurrentBarrelGoo;
        }
	}
}
