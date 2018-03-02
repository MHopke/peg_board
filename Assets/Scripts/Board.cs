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

    bool CheckForOpenMoves()
    {
        
        for (int index = 0; index < Slots.Length; index++)
        {
            _slotIter = Slots[index];

            if (_slotIter.HasPeg)
            {
                //check if there is a peg left to it AND an open space next to that
                /*if (_slotIter.IndexInRow >= 2 && Slots[_slotIter.IndexInRow - 1].HasPeg
                    && !Slots[_slotIter.IndexInRow - 2].HasPeg)
                    return true;
                //check if there is a peg right to it AND an open space next to that
                else if (_slotIter.IndexInRow >= 0 && _slotIter.IndexInRow + 2 < _slotIter.RowIndex
                        && Slots[_slotIter.IndexInRow + 1].HasPeg && !Slots[_slotIter.IndexInRow + 2].HasPeg)
                    return true;
                else if(*/
                
                //check if there is a peg above right AND an open space after it
                //check if there is a peg above left AND an open space after it
                //check if there is a peg below right AND an open space after it
                //check if there is a peg below left AND an open space after it
            }
        }

        return false;
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
