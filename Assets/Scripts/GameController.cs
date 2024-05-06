using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ItemsSpritsConfig _itemsSpritsConfig;
    [SerializeField] private ItemsSpritsComboBonusConfig _itemsSpritsComboBonusConfig;
    [SerializeField] private RectTransform _playZone;
    [SerializeField] private RectTransform _comboZone;
    [SerializeField] private ComboSlot _comboSlotPrefab;
    [SerializeField] private ItemCombo _itemCombo;
    [SerializeField] private Item _itemPrefab;
    [SerializeField] private Slot _slotPrefab;

    public const int _slotCount = 8;
    private Vector2 slotSize;

    Slot[,] slots = new Slot[_slotCount, _slotCount];
    public Slot[,] Slots
    {
        get { return slots; }
        set { slots = value; }
    }

    private ComboSlot comboSlot;

    void Start()
    {
        slotSize = CalculateSlotSize();
        CreateSlots();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateComboItem();
        }
    }

    private void CreateSlots()
    {
        for (int row = 0; row < _slotCount; row++)
        {
            for (int col = 0; col < _slotCount; col++)
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
        item.Init(id, _itemsSpritsConfig.ItemsSprites[id], slotSize);
        item.transform.SetParent(slot.transform);
        item.transform.localPosition = Vector3.zero;
        slot.SetItem(item);
    }

    /*private ComboSlot CreateComboSlot()
    {
        ComboSlot slot = Instantiate(_comboSlotPrefab, _comboZone);
        slot.Init(slotSize);
        CreateComboItem(slot);
        return slot;
    }*/

    private void CreateComboItem()
    {
        var id = Random.Range(0, _itemsSpritsComboBonusConfig.ItemsSprites.Count);
        _itemCombo.Init(id, _itemsSpritsComboBonusConfig.ItemsSprites[id], _comboZone.rect.size);
        //slot.SetItem(item);
    }

   

    private Vector2 CalculateSlotSize()
    {
        Vector2 zoneSize = _playZone.rect.size;
        float slotWidth = zoneSize.x / _slotCount;
        float slotHeight = zoneSize.y / _slotCount;
        return new Vector2(slotWidth, slotHeight);
    }

    private void OnHorizontalSwap(Slot slot, int direction)
    {
        var x = slot.xPos + direction;
        if (x < 0 || x >= _slotCount) return;

        var targetSlot = slots[slot.yPos, x];
        slot.item.Move(targetSlot);
        targetSlot.item.Move(slot);
    }

    private void OnVerticalSwap(Slot slot, int direction)
    {
        var y = slot.yPos + direction;
        if (y < 0 || y >= _slotCount) return;
        var targetSlot = slots[y, slot.xPos];
        slot.item.Move(targetSlot);
        targetSlot.item.Move(slot);
    }
}
