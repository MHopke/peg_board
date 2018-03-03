using UnityEngine.UI;

public class EndView : UIView
{
    #region Constants
    const string WIN = "CONGRATULATIONS YOU WON!!!!";
    const string LOSE = "SORRY, TRY AGAIN!";
    #endregion

    #region Public Vars
    public Text StatusText;
    #endregion

    #region Methods
    public void SetWinStatus(bool won)
    {
        if (won)
            StatusText.text = WIN;
        else
            StatusText.text = LOSE;
    }
    #endregion
}
