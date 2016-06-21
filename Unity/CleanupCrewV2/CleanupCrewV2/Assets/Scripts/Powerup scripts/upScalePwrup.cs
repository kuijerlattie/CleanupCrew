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
         paddle.transform.localScale = new Vector3(11, 1, 1); 
        
    }

    static public void ResetScale()
    {
         GameObject paddle;

         paddle = FindObjectOfType<PaddleControls>().gameObject;
         paddle.transform.localScale = new Vector3(8, 1, 1);
        
    }

}
