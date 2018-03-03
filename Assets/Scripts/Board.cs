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
    EmptySlot _slotIter;
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

    public void Restart()
    {
        int index = 0;

        EmptySlot slot = null;
        for (index = 0; index < Slots.Length; index++)
        {
            slot = Slots[index];
            slot.Index = index;
            slot.ClearPeg();
        }

        Peg peg = null;
        _pegCount = 0;
        for (index = Slots.Length - 1; index > 0; index--)
        {
            peg = Pegs[_pegCount];
            peg.SetInactive();
            Slots[index].SetPeg(peg);
            _pegCount++;
        }
    }

    bool CheckForNoMoreMoves()
    {
        int midSlotIndex = 0, sub =0;
        for (int index = 0; index < Slots.Length; index++)
        {
            _slotIter = Slots[index];

            //Debug.Log(_slotIter.Index + " " + _slotIter.HasPeg);
            if (_slotIter.HasPeg)
            {
                for (sub = 0; sub < Slots.Length; sub++)
                {
                    if (index != sub)
                    {
                        midSlotIndex = (index + sub) / 2;
                        //Debug.Log(_slotIter.Index + " " + Slots[midSlotIndex].Index + " " + Slots[sub].Index);
                        if (CanMove(_slotIter, Slots[midSlotIndex], Slots[sub]))
                            return false;
                    }
                }
            }
        }

        return true;
    }
    bool CanMove(EmptySlot startSlot, EmptySlot midSlot, EmptySlot target)
    {
        return midSlot.HasPeg && //check if the middle index has a peg 
                      ((startSlot.RowIndex == target.RowIndex && Mathf.Abs(startSlot.IndexInRow - target.IndexInRow) == SEPARATION) || //check if they are in the same row and 2 separate
                       (Mathf.Abs(startSlot.RowIndex - target.RowIndex) == SEPARATION && startSlot.IndexInRow % 2 == target.IndexInRow % 2)); //check if they are in separate rows which are 2 apart && are the same parity (even or odd)
    }
    void EndGame(bool won)
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
            gameOver(won);
    }
    #endregion

    #region Event Listeners
    void SlotClicked(EmptySlot slot)
    {
        int midSlotIndex = (_currentPeg.Slot.Index + slot.Index) / 2;
        EmptySlot midSlot = Slots[midSlotIndex];
        //Debug.Log("start index: " + _currentPeg.Slot.Index + " target index: " + slot.Index + " middle slot: " +  midSlotIndex);
        if(CanMove(_currentPeg.Slot, midSlot, slot)) 
        {
            _currentPeg.Slot.ClearPeg();
            slot.SetPeg(_currentPeg);

            _currentPeg.SetInactive();
            midSlot.ClearPeg();

            _pegCount--;

            if (_pegCount == PEGS_LEFT_TO_WIN)
                EndGame(true);
            else if (CheckForNoMoreMoves())
                EndGame(false);
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
