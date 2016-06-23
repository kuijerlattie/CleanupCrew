using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour {
    
    Text tPoints;

    Image EnergyGauge;
    Image BarrelGauge;
    Image BossGauge;
    Image BossBackground;

    // Use this for initialization
    void Start () {
        tPoints = GameObject.Find("Points").GetComponent<Text>(); //final
        EnergyGauge = GameObject.Find("EnergyGauge").GetComponent<Image>(); //final
        BarrelGauge = GameObject.Find("BarrelGauge").GetComponent<Image>(); //final
        BossGauge = GameObject.Find("BossGauge").GetComponent<Image>(); //final
        BossBackground = GameObject.Find("BossBackground").GetComponent<Image>();//final

    }
	
	// Update is called once per frame
	void Update () {
        tPoints.text = GameManager.instance.CurrentPoints.ToString();
        EnergyGauge.fillAmount = GameManager.instance.CurrentEnergyUI;
        BarrelGauge.fillAmount = GameManager.instance.CurrentBarrelGooUI;

        if (GameManager.instance.CurrentGamestate == GameManager.gamestate.Boss)
        {
            BossGauge.enabled = true;
            BossBackground.enabled = true;
            BossGauge.fillAmount = GameObject.FindObjectOfType<BossBase>().hitpointsForHud;
        }
        else
        {
            BossGauge.enabled = false;
            BossBackground.enabled = false;
        }
	}
}
