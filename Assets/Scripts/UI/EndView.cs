using UnityEngine.UI;

public class EndView : UIView
{
    #region Constants
    const string WIN = "CONGRATULATIONS\nYou won!!!";
    const string LOSE = "Sorry, Try Again!";
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
