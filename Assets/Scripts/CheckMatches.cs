using UnityEngine;

public class CheckMatches : MonoBehaviour //IMatchChecker
{
    [SerializeField] private SlotManager _slotManager;
    [SerializeField] private ComboItemManager _comboItemManager;
    [SerializeField] private GameController _gameController;

   /* public void CheckGrid()
    {
        List<Slot> toBeBurned = CheckVerticalLines();
        if (toBeBurned.Count > 0)
        {
            GetItemIdsFromSlots(toBeBurned);
            BurnItems(toBeBurned);
            _slotManager.RefillSlots();
            CheckGrid();
            return;
        }

        List<Slot> toBeBurnedHor = CheckHorizontalLines();
        if (toBeBurnedHor.Count > 0)
        {
            GetItemIdsFromSlots(toBeBurnedHor);
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
                    consecutiveCount = 1;
                    toBeBurned.Clear();
                }
            }
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
                    consecutiveCount = 1;
                    toBeBurned.Clear();
                }
            }
        }
        return new List<Slot>();
    }

    private void BurnItems(List<Slot> slots)
    {
        foreach (var slot in slots)
        {
            Destroy(slot.item.gameObject);
            slot.item = null;
        }
    }

    public void GetItemIdsFromSlots(List<Slot> slots)
    {
        List<int> itemIds = new List<int>();
        foreach (Slot slot in slots)
        {
            if (slot != null && slot.item != null)
            {
                itemIds.Add(slot.item.Id);  // Додавання ID айтема до списку
            }
        }
        _gameController.GetBurnedItemIds(itemIds[0]); 
    }
*/
}
