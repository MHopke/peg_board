using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peg : MonoBehaviour
{
    #region Events
    public event System.Action<Peg> clicked;
    #endregion

    #region Public Vars
    public SpriteRenderer Rend;
    #endregion

    #region Private Vars
    EmptySlot _slot;

    static Color ACTIVE_COLOR = new Color(0f, 192f / 255f, 231f / 255f, 1f);
    static Color NORMAL_COLOR = new Color(0f, 161f / 255f, 193f / 255f, 1f);
    #endregion

    #region UI Methods
    public void OnClick()
    {
        if (clicked != null)
            clicked(this);
    }
    #endregion

    #region Methods
    public void SetActive()
    {
        Rend.color = ACTIVE_COLOR;
    }
    public void SetInactive()
    {
        Rend.color = NORMAL_COLOR;
    }
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }
    public void SetSlot(EmptySlot slot)
    {
        _slot = slot;
    }
    #endregion

    #region Properties
    public EmptySlot Slot
    {
        get { return _slot; }
    }
    #endregion
}
