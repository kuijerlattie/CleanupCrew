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
        tState = GameObject.Find("GameState").GetComponent<Text>();
        tGameplayState = GameObject.Find("GameplayState").GetComponent<Text>();
        EnergyGauge = GameObject.Find("EnergyGauge").GetComponent<Image>();
        BarrelGauge = GameObject.Find("BarrelGauge").GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        tPoints.text = "points: " + GameManager.instance.CurrentPoints;
        tEnergy.text = "energy: " + GameManager.instance.CurrentEnergy;
        tWaterpoints.text = "Goo in barrel: " + GameManager.instance.CurrentBarrelGoo;
        tState.text = "Current state: " + GameManager.instance.CurrentGamestate;
        tGameplayState.text = "current gameplay state: " + GameManager.instance.CurrentGameplaystate;
        Debug.Log(GameManager.instance.CurrentBarrelGooUI);
        EnergyGauge.fillAmount = GameManager.instance.CurrentEnergyUI;
        BarrelGauge.fillAmount = GameManager.instance.CurrentBarrelGooUI;
	}
}
