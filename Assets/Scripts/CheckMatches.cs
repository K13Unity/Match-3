using System.Collections.Generic;
using UnityEngine;

public class CheckMatches : MonoBehaviour, IMatchChecker
{
    [SerializeField] private SlotManager _slotManager;


    public void CheckGrid()
    {
        List<Slot> toBeBurned = CheckVerticalLines();
        if (toBeBurned.Count > 0)
        {
            BurnItems(toBeBurned);
            _slotManager.RefillSlots();
            CheckGrid();
            return;
        }

        List<Slot> toBeBurnedHor = CheckHorizontalLines();
        if (toBeBurnedHor.Count > 0)
        {
            BurnItems(toBeBurnedHor);
            _slotManager.RefillSlots();
            CheckGrid();
            return;
        }
    }

    private List<Slot> CheckHorizontalLines()
    {
        for (int row = 0; row < GameController.slotCount; row++)
        {
            int consecutiveCount = 1;
            List<Slot> toBeBurned = new List<Slot>();

            for (int col = 1; col < GameController.slotCount; col++)
            {
                if (_slotManager.Slots[row, col].item.Id == _slotManager.Slots[row, col - 1].item.Id)
                {
                    if (consecutiveCount == 1)
                    {
                        toBeBurned.Add(_slotManager.Slots[row, col - 1]);
                    }
                    toBeBurned.Add(_slotManager.Slots[row, col]);
                    consecutiveCount++;
                }
                else
                {
                    if (toBeBurned.Count >= 3)
                    {
                        return toBeBurned;
                    }
                    //ProcessBurnItems(consecutiveCount, toBeBurned);
                    consecutiveCount = 1;
                    toBeBurned.Clear();
                }
            }
            //ProcessBurnItems(consecutiveCount, toBeBurned);
        }
        return new List<Slot>();
    }

    private List<Slot> CheckVerticalLines()
    {
        for (int col = 0; col < GameController.slotCount; col++)
        {
            int consecutiveCount = 1;
            List<Slot> toBeBurned = new List<Slot>();

            for (int row = 1; row < GameController.slotCount; row++)
            {
                if (_slotManager.Slots[row, col].item.Id == _slotManager.Slots[row - 1, col].item.Id)
                {
                    if (consecutiveCount == 1)
                    {
                        toBeBurned.Add(_slotManager.Slots[row - 1, col]);
                    }
                    toBeBurned.Add(_slotManager.Slots[row, col]);
                    consecutiveCount++;
                }
                else
                {
                    if(toBeBurned.Count >= 3)
                    {
                        return toBeBurned;
                    }
                   // ProcessBurnItems(consecutiveCount, toBeBurned);
                    consecutiveCount = 1;
                    toBeBurned.Clear();
                }

            }
            //ProcessBurnItems(consecutiveCount, toBeBurned);
        }
        return new List<Slot>();
    }

    private void ProcessBurnItems(int consecutiveCount, List<Slot> toBeBurned)
    {
        if (consecutiveCount >= 3)
        {
            BurnItems(toBeBurned);
        }
    }

    private void BurnItems(List<Slot> slots)
    {
        foreach (var slot in slots)
        {
            Destroy(slot.item.gameObject);
            slot.item = null;
        }
    }
}
