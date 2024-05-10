using System.Collections.Generic;
using UnityEngine;

public class CheckMatches : MonoBehaviour, IMatchChecker
{
    [SerializeField] private SlotManager _slotManager;



    public void CheckGrid()
    {
        Debug.Log("CheckGrid");
        CheckHorizontalLines();
        CheckVerticalLines();
    }

    private void CheckHorizontalLines()
    {
        for (int row = 0; row < GameController.slotCount; row++)
        {
            int consecutiveCount = 1;
            List<Item> toBeBurned = new List<Item>();

            for (int col = 1; col < GameController.slotCount; col++)
            {
                if (_slotManager.Slots[row, col].item.Id == _slotManager.Slots[row, col - 1].item.Id)
                {
                    if (consecutiveCount == 1)
                    {
                        toBeBurned.Add(_slotManager.Slots[row, col - 1].item);
                    }
                    toBeBurned.Add(_slotManager.Slots[row, col].item);
                    consecutiveCount++;
                }
                else
                {
                    if (consecutiveCount >= 3)
                    {
                        BurnItems(toBeBurned);
                    }
                    consecutiveCount = 1;
                    toBeBurned.Clear();
                }
            }
            if (consecutiveCount >= 3)
            {
                BurnItems(toBeBurned);
            }
        }
    }

    private void CheckVerticalLines()
    {
        for (int col = 0; col < GameController.slotCount; col++)
        {
            int consecutiveCount = 1;
            List<Item> toBeBurned = new List<Item>();

            for (int row = 1; row < GameController.slotCount; row++)
            {
                if (_slotManager.Slots[row, col].item.Id == _slotManager.Slots[row - 1, col].item.Id)
                {
                    if (consecutiveCount == 1)
                    {
                        toBeBurned.Add(_slotManager.Slots[row - 1, col].item);
                    }
                    toBeBurned.Add(_slotManager.Slots[row, col].item);
                    consecutiveCount++;
                }
                else
                {
                    if (consecutiveCount >= 3)
                    {
                        BurnItems(toBeBurned);
                    }
                    consecutiveCount = 1;
                    toBeBurned.Clear();
                }
            }
            if (consecutiveCount >= 3)
            {
                BurnItems(toBeBurned);
            }
        }
    }

    private void BurnItems(List<Item> items)
    {
        foreach (var item in items)
        {
            Destroy(item.gameObject);
        }
    }
}
