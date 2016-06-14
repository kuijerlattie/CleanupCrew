using UnityEngine;
using System.Collections;
using System;

public class destroyBlobs : PwrupBase {

    public PwrupManager.PowerupType powerupType;

    public override void startPwrup()
    {
        PwrupManager manager = FindObjectOfType<PwrupManager>();

        manager.ActivatePwrup(powerupType);
    }

    public override void stopPwrup()
    {
        
    }

    public static void destroyAllBlobs()
    {
        BlobScript[] blobs = FindObjectsOfType<BlobScript>();
        
        for(int i = 0; i < blobs.Length; i++)
        {
            Destroy(blobs[i].gameObject);
        }
    }
}
