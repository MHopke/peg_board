using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartView : UIView
{
    #region Public Vars
    public Board GameBoard;
    #endregion

    #region UI Methods
    public void StartGame()
    {
        GameBoard.StartGame();
        Deactivate();
    }
    #endregion
}
