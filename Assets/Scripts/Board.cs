using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    #region Events
    public event System.Action<bool> gameOver;
    #endregion

    #region Constants
    const int SEPARATION = 2;
    const int PEGS_LEFT_TO_WIN = 1;
    #endregion

    #region Public Vars
    public EmptySlot[] Slots;
    public Peg[] Pegs;
    #endregion

    #region Private Vars
    int _pegCount;
    Peg _currentPeg;
    #endregion 

    #region Methods
    public void StartGame()
    {
        int index = 0;

        EmptySlot slot = null;
        for (index = 0; index < Slots.Length; index++)
        {
            slot = Slots[index];
            slot.clicked += SlotClicked;
            slot.Index = index;
        }

        Peg peg = null;
        _pegCount = 0;
        for (index = Slots.Length - 1; index >0; index--)
        {
            peg = Pegs[_pegCount];
            peg.clicked += PegClicked;
            Slots[index].SetPeg(peg);
            _pegCount++;
        }
    }
    public void EndGame()
    {
        int index = 0;
        for (index = 0; index < Slots.Length; index++)
        {
            Slots[index].clicked -= SlotClicked;
        }
        for (index = 0; index < Pegs.Length; index++)
        {
            Pegs[index].clicked -= PegClicked;
        }

        if (gameOver != null)
            gameOver(true);
    }
    public void Restart()
    {
        int index = 0;

        EmptySlot slot = null;
        for (index = 0; index < Slots.Length; index++)
        {
            slot = Slots[index];
            slot.Index = index;
        }

        Peg peg = null;
        _pegCount = 0;
        for (index = Slots.Length - 1; index > 0; index--)
        {
            peg = Pegs[_pegCount];
            Slots[index].SetPeg(peg);
            _pegCount++;
        }
    }
    #endregion

    #region Event Listeners
    void SlotClicked(EmptySlot slot)
    {
        int midSlotIndex = (_currentPeg.Slot.Index + slot.Index) / 2;
        EmptySlot midSlot = Slots[midSlotIndex];
        //Debug.Log("start index: " + _currentPeg.Slot.Index + " target index: " + slot.Index + " middle slot: " +  midSlotIndex);
        if (midSlot.HasPeg && //check if the middle index has a peg 
            (_currentPeg.Slot.RowIndex == slot.RowIndex && Mathf.Abs(_currentPeg.Slot.IndexInRow - slot.IndexInRow) == SEPARATION) || //check if they are in the same row and 2 separate
            (Mathf.Abs(_currentPeg.Slot.RowIndex - slot.RowIndex) == SEPARATION && _currentPeg.Slot.IndexInRow % 2 == slot.IndexInRow %2)) //check if they are in separate rows which are 2 apart && are the same parity (even or odd)
        {
            _currentPeg.Slot.ClearPeg();
            slot.SetPeg(_currentPeg);

            _currentPeg.SetInactive();
            midSlot.ClearPeg();

            _pegCount--;

            if (_pegCount == PEGS_LEFT_TO_WIN)
                EndGame();
        }
    }
    void PegClicked(Peg peg)
    {
        if (_currentPeg != null)
            _currentPeg.SetInactive();

        _currentPeg = peg;

        _currentPeg.SetActive();
    }
    #endregion
}
