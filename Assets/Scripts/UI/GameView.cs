using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameView : UIView
{
    #region Events
    public event System.Action restartEvent;
    #endregion

    #region UI Methods
    public void Restart()
    {
        if (restartEvent != null)
            restartEvent();
    }
    #endregion
}
