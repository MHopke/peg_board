using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    #region Public Vars
    public StartView StartV;
    public GameView GameV;
    public EndView EndV;

    public Board GameBoard;
    #endregion

    #region Unity Methods
    void Awake()
    {
        StartV.deactivated += StartDeactivated;
        EndV.deactivated += EndDeactivated;
        GameBoard.gameOver += GameOver;
        GameV.restartEvent += Restart;

        EndV.Deactivate();
        GameV.Deactivate();
    }
    void OnDestroy()
    {
        StartV.deactivated -= StartDeactivated;
        EndV.deactivated -= EndDeactivated;
        GameBoard.gameOver -= GameOver;
        GameV.restartEvent -= Restart;
    }
    #endregion

    #region Event Listeners
    void StartDeactivated()
    {
        GameBoard.StartGame();
        GameV.Activate();
    }
    void GameOver(bool won)
    {
        GameV.Deactivate();
        EndV.Activate();
    }
    void EndDeactivated()
    {
        StartV.Activate();
    }
    void Restart()
    {
        GameBoard.Restart();
    }
    #endregion
}
