using UnityEngine;


public class downScalePwrup : PwrupBase {
    public PwrupManager.PowerupType powerupType;
    public override void startPwrup()
    {
        PwrupManager manager = FindObjectOfType<PwrupManager>();

        manager.ActivatePwrup(powerupType);
        FindObjectOfType<PaddleControls>().SetBoundaries();
    }

    public override void stopPwrup()
    {
        ResetScale();
        
    }

    static public void Narrow()
    {
        GameObject paddle;

        paddle = FindObjectOfType<PaddleControls>().gameObject;
        paddle.transform.localScale = new Vector3(5, 1, 1);
    }

    static public void ResetScale()
    {
        GameObject paddle;

        paddle = FindObjectOfType<PaddleControls>().gameObject;
        paddle.transform.localScale = new Vector3(8, 1, 1);
        FindObjectOfType<PaddleControls>().SetBoundaries();
    }
}
