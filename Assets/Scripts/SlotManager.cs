using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [SerializeField] private ItemsSpritsConfig _itemsSpritsConfig;
    [SerializeField] private RectTransform _playZone;
    [SerializeField] private Slot _slotPrefab;
    [SerializeField] private Item _itemPrefab;


    public const int _slotCount = 8;
    private Vector2 _slotSize;
    Slot[,] slots = new Slot[_slotCount, _slotCount];
    public Slot[,] Slots
    {
        get { return slots; }
        set { slots = value; }
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
        var id = Random.Range(0, _itemsSpritsConfig.ItemsSprites.Count);
        Item item = Instantiate(_itemPrefab);
        item.Init(id, _itemsSpritsConfig.ItemsSprites[id], _slotSize);
        item.transform.SetParent(slot.transform);
        item.transform.localPosition = Vector3.zero;
        slot.SetItem(item);
    }

    private void OnHorizontalSwap(Slot slot, int direction)
    {
        var x = slot.xPos + direction;
        if (x < 0 || x >= _slotCount) return;

        var targetSlot = slots[slot.yPos, x];
        slot.item.Move(targetSlot);
        targetSlot.item.Move(slot, () => CheckGrid(slot));
    }

    private void OnVerticalSwap(Slot slot, int direction)
    {
        var y = slot.yPos + direction;
        if (y < 0 || y >= _slotCount) return;
        var targetSlot = slots[y, slot.xPos];
        slot.item.Move(targetSlot);
        targetSlot.item.Move(slot,() => CheckGrid(slot));
    }

    public void CheckGrid(Slot slot)
    {
        var id = slot.item.Id;

        List<Slot> slots = new List<Slot>();
        slots.Add(slot);
        if (slot.xPos - 1 >= 0)
        {
            var prevSlot = Slots[slot.xPos - 1, slot.yPos];
            Debug.Log(prevSlot.yPos +"" + prevSlot.xPos);
            if(prevSlot.item.Id == id)
            {
                slots.Add(prevSlot);
                if(slot.xPos - 2 >= 0)
                {
                    var prevPrevSlot = Slots[slot.xPos - 2, slot.yPos];
                    Debug.Log(prevPrevSlot.yPos + "" + prevPrevSlot.xPos);
                    if (prevPrevSlot.item.Id == id)
                    {
                        slots.Add(prevPrevSlot);
                    }
                }
            }
        }
        if (slot.xPos + 1 < _slotCount)
        {
            var nextSlot = Slots[slot.xPos + 1, slot.yPos];
            if(nextSlot.item.Id == id)
            {
                slots.Add(nextSlot);
                if(slot.xPos + 2 < _slotCount)
                {
                    var nextNextSlot = Slots[slot.xPos + 2, slot.yPos];
                    if(nextNextSlot.item.Id == id)
                    {
                        slots.Add(nextNextSlot);
                    }
                }
            }
        }
        if(slots.Count >= 3)
        {
            foreach (var s in slots)
            {
                Debug.Log(slot.yPos + ", " + slot.xPos + " cleared!");
            }
        }
    }
}
