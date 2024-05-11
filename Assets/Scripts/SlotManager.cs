using UnityEngine;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private ItemsSpritsConfig _itemsSpritsConfig;
    [SerializeField] private RectTransform _playZone;
    [SerializeField] private Slot _slotPrefab;
    [SerializeField] private Item _itemPrefab;

    private IMatchChecker _matchChecker;

    private Vector2 _slotSize;
    Slot[,] slots = new Slot[GameController.slotCount, GameController.slotCount];
    public Slot[,] Slots
    {
        get { return slots; }
        set { slots = value; }
    }

    public void SetMatchChecker(IMatchChecker matchChecker)
    {
        _matchChecker = matchChecker;
    }


    public void CreateSlots(int rowCount, int colCount, Vector2 slotSize)
    {
        _slotSize = slotSize;
        for (int row = 0; row < rowCount; row++)
        {
            for (int col = 0; col < colCount; col++)
            {
                Slot slot = Instantiate(_slotPrefab, _playZone);
                float xPos = col * slotSize.x + slotSize.x / 2 - _playZone.rect.size.x / 2;
                float yPos = row * slotSize.y + slotSize.y / 2 - _playZone.rect.size.y / 2;
                slot.Init(col, row, new Vector2(slotSize.x, slotSize.y));
                slot.transform.localPosition = new Vector2(xPos, yPos);
                slots[row, col] = slot;
                CreateItem(slot);
                slot.onHorisontal += OnHorizontalSwap;
                slot.onVertical += OnVerticalSwap;
            }
        }
    }

    private void CreateItem(Slot slot)
    {

        int id;
        bool matchFound;
        do
        {
            matchFound = false;
            id = Random.Range(0, _itemsSpritsConfig.ItemsSprites.Count);

            if (slot.xPos >= 2)
            {
                if (slots[slot.yPos, slot.xPos - 1].item.Id == id &&
                    slots[slot.yPos, slot.xPos - 2].item.Id == id)
                {
                    matchFound = true;
                }
            }

            if (slot.yPos >= 2)
            {
                if (slots[slot.yPos - 1, slot.xPos].item.Id == id &&
                    slots[slot.yPos - 2, slot.xPos].item.Id == id)
                {
                    matchFound = true;
                }
            }
        } while (matchFound);
        Item item = Instantiate(_itemPrefab);
        item.Init(id, _itemsSpritsConfig.ItemsSprites[id], _slotSize);
        item.transform.SetParent(slot.transform);
        item.transform.localPosition = Vector3.zero;
        slot.SetItem(item);
    }

    private void AddingNewItems(Slot slot)
    {
        int id;
        id = Random.Range(0, _itemsSpritsConfig.ItemsSprites.Count);
        Item item = Instantiate(_itemPrefab);
        item.Init(id, _itemsSpritsConfig.ItemsSprites[id], _slotSize);
        item.transform.SetParent(slot.transform);
        item.transform.localPosition = Vector3.zero;
        slot.SetItem(item);
    }

    private void OnHorizontalSwap(Slot slot, int direction)
    {
        var x = slot.xPos + direction;
        if (x < 0 || x >= GameController.slotCount) return;

        var targetSlot = slots[slot.yPos, x];
        slot.item.Move(targetSlot);
        targetSlot.item.Move(slot, () => {
            _matchChecker.CheckGrid();
        });
    }

    private void OnVerticalSwap(Slot slot, int direction)
    {
        var y = slot.yPos + direction;
        if (y < 0 || y >= GameController.slotCount) return;
        var targetSlot = slots[y, slot.xPos];
        slot.item.Move(targetSlot);
        targetSlot.item.Move(slot, () => {
           _matchChecker.CheckGrid();
        });
    }

    public void RefillSlots()
    {
        Debug.Log("RefillSlots");
        for (int x = 0; x < GameController.slotCount; x++)
        {
            int emptyCount = 0;
            for (int y = 0; y < GameController.slotCount; y++)
            {
                if (slots[y, x].item == null)
                {
                    emptyCount++;
                    
                }
                else if (emptyCount > 0)
                {
                    slots[y - emptyCount, x].SetItem(slots[y, x].item);
                    slots[y, x].SetItem(null);
                    y -= emptyCount;
                    emptyCount = 0;
                }
            }

            for (int y = 0; y < emptyCount; y++)
            {
                AddingNewItems(slots[GameController.slotCount - 1 - y, x]);
            }
        }
    }
}
