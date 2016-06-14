using UnityEngine;


public class upScalePwrup : PwrupBase {
    public PwrupManager.PowerupType powerupType;

    public override void startPwrup()
    {
        PwrupManager manager = FindObjectOfType<PwrupManager>();
        
        manager.ActivatePwrup(powerupType);
    }

    public override void stopPwrup()
    {
        ResetScale();
    }

    static public void Wider()
    {
        GameObject paddle;

        paddle = FindObjectOfType<PaddleControls>().gameObject;
        paddle.transform.localScale = new Vector3(6, 1, 0.5f);
    }

    static public void ResetScale()
    {
        GameObject paddle;

        paddle = FindObjectOfType<PaddleControls>().gameObject;
        paddle.transform.localScale = new Vector3(4, 1, 0.5f);
    }
}
