using UnityEngine;

public class ProgressSlotManager : MonoBehaviour
{
    [SerializeField] private ItemsSpritesProgressConfig _itemsSpritesProgressConfig;
    [SerializeField] private ProgressSlot _progressSlotPrefab;
    [SerializeField] private ItemProgress _itemProgressPrefab;
    [SerializeField] private RectTransform _progressZone;

    private int _progressCount = 1;

    private Vector2 _slotSize;


    public void CreateProgressSlots(int slotCount, Vector2 slotSize)
    {
        _slotSize = slotSize;
        float slotInterval = slotSize.y / 6;
        for (int i = 0; i < slotCount; i++)
        {
            ProgressSlot slot = Instantiate(_progressSlotPrefab, _progressZone);
            float yPos = i * slotInterval * 2 + slotInterval - _progressZone.rect.size.y / 2;
            slot.Init(_progressCount, i,  new Vector2(slotSize.x, slotSize.y));
            slot.transform.localPosition = new Vector2(0, yPos);
            CreateItem(slot);
        }
    }

    private void CreateItem(ProgressSlot slot)
    {
        var id = Random.Range(0, _itemsSpritesProgressConfig.ItemsSprites.Count);
        ItemProgress item = Instantiate(_itemProgressPrefab);
        item.Init(id, _itemsSpritesProgressConfig.ItemsSprites[id], _slotSize);
        item.transform.SetParent(slot.transform);
        item.transform.localPosition = Vector3.zero;
        slot.SetItem(item);
    }
}
