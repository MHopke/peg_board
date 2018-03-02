using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptySlot : MonoBehaviour
{
    #region Events
    public event System.Action<EmptySlot> clicked;
    #endregion

    #region Constants
    const float OFF_SCREEN = 99f;
    #endregion

    #region Public Vars
    public int Index;
    public int RowIndex;
    public int IndexInRow;
    #endregion

    #region Private Vars
    Peg _peg;
    #endregion

    #region UI Methods
    public void OnClick()
    {
        if (_peg != null)
            return;

        if (clicked != null)
            clicked(this);
    }
    #endregion

    #region Methods
    public void SetPeg(Peg peg)
    {
        _peg = peg;
        _peg.SetSlot(this);
        peg.SetPosition(transform.position);
    }
    public void ClearPeg()
    {
        if (_peg != null)
        {
            _peg.SetSlot(null);
            _peg.SetPosition(new Vector3(OFF_SCREEN, OFF_SCREEN, OFF_SCREEN));
            _peg = null;
        }
    }
    #endregion

    #region Properties
    public bool HasPeg
    {
        get { return _peg != null; }
    }
    #endregion
}
