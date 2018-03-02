using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    #region Events
    public event System.Action deactivated;
    #endregion

    #region Methods
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Deactivate()
    {
        gameObject.SetActive(false);

        if (deactivated != null)
            deactivated();
    }
    #endregion
}
