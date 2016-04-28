using UnityEngine;
using System.Collections;

public class GameSettings {

    static int VSYNC = 0;    //0 = no vsync, 1 = sync every frame (60fps), 2 = sync every other frame (30fps)
	
    static public void ApplySettings()
    {
        QualitySettings.vSyncCount = VSYNC;

    }
}
