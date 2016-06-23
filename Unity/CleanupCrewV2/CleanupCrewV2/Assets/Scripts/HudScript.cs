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
    Image BossGauge;
    Image BossBackground;

    // Use this for initialization
    void Start () {
        tPoints = GameObject.Find("Points").GetComponent<Text>(); //final
        tEnergy = GameObject.Find("Energy").GetComponent<Text>(); //debug
        tWaterpoints = GameObject.Find("Waterpoints").GetComponent<Text>(); //debug
        tEarthpoints = GameObject.Find("Sandpoints").GetComponent<Text>(); //debug  
        tSpacepoints = GameObject.Find("Spacepoints").GetComponent<Text>(); //debug
        tState = GameObject.Find("GameState").GetComponent<Text>(); //debug 
        tGameplayState = GameObject.Find("GameplayState").GetComponent<Text>(); //debug
        EnergyGauge = GameObject.Find("EnergyGauge").GetComponent<Image>(); //final
        BarrelGauge = GameObject.Find("BarrelGauge").GetComponent<Image>(); //final
        BossGauge = GameObject.Find("BossGauge").GetComponent<Image>(); //final
        BossBackground = GameObject.Find("BossBackground").GetComponent<Image>();//final

    }
	
	// Update is called once per frame
	void Update () {
        tPoints.text = GameManager.instance.CurrentPoints.ToString();
        tEnergy.text = "energy: " + GameManager.instance.CurrentEnergy;
        tState.text = "Current state: " + GameManager.instance.CurrentGamestate;
        tGameplayState.text = "current gameplay state: " + GameManager.instance.CurrentGameplaystate;
        EnergyGauge.fillAmount = GameManager.instance.CurrentEnergyUI;
        BarrelGauge.fillAmount = GameManager.instance.CurrentBarrelGooUI;

        if (GameManager.instance.CurrentGamestate == GameManager.gamestate.Boss)
        {
            BossGauge.enabled = true;
            BossBackground.enabled = true;
            BossGauge.fillAmount = GameObject.FindObjectOfType<BossBase>().hitpointsForHud;
            tEarthpoints.text = "Boss health: " + GameObject.FindObjectOfType<MoleScript>().hitpoints;
            tSpacepoints.text = "Boss state: " + GameObject.FindObjectOfType<MoleScript>().state;
            tWaterpoints.text = "Boss invincible: " + FindObjectOfType<MoleScript>().invincible;
        }
        else
        {
            BossGauge.enabled = false;
            BossBackground.enabled = false;
            tEarthpoints.text = "No Boss";
            tWaterpoints.text = "Goo in barrel: " + GameManager.instance.CurrentBarrelGoo;
        }
	}
}
