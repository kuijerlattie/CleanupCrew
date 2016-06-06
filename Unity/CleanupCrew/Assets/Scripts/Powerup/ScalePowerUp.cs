using UnityEngine;
using System.Collections;

public class ScalePowerUp : MonoBehaviour {

    static Vector3 basescale = new Vector3(GameSettings.PaddleScaleXS,1,2);
    
    static public void ScaleDown()
    {
        foreach (GameObject p in SI.Gamemanager.paddles)
        {
            p.transform.localScale = basescale - new Vector3(GameSettings.PowerupPaddleDownscaleByS, 0, 0);
        }
    }

    static public void ScaleUp()
    {
        foreach (GameObject p in SI.Gamemanager.paddles)
        {
            p.transform.localScale = basescale + new Vector3(GameSettings.PowerupPaddleUpscaleByS, 0, 0);
        }
    }

    static public void ResetScale()
    {
        foreach (GameObject p in SI.Gamemanager.paddles)
        {
            p.transform.localScale = basescale;
        }
    }
}
