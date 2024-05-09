using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ProgressSlotManager _progressSlotManager;
    [SerializeField] private ComboItemManager _comboItemManager;
    [SerializeField] private SlotManager _slotManager;

    [SerializeField] private RectTransform _progressZone;
    [SerializeField] private RectTransform _playZone;


    private int _heightProgressCount = 15;
    public const int _slotCount = 8;

    private Vector2 _slotSize;
    private Vector2 _progressSlotSize;


    void Start()
    {
        _slotSize = CalculateSlotSize();
        _progressSlotSize = CalculateProgressSlotSize();
        _slotManager.CreateSlots(_slotCount, _slotCount, _slotSize);
        _progressSlotManager.CreateProgressSlots(_heightProgressCount, _progressSlotSize);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _comboItemManager.CreateComboItem();
        }
    }

    private Vector2 CalculateSlotSize()
    {
        Vector2 zoneSize = _playZone.rect.size;
        float slotWidth = zoneSize.x / _slotCount;
        float slotHeight = zoneSize.y / _slotCount;
        return new Vector2(slotWidth, slotHeight);
    }

    private Vector2 CalculateProgressSlotSize()
    {
        Vector2 zoneSize = _progressZone.rect.size;
        float slotSize = Mathf.Min(zoneSize.x, zoneSize.y);
        return new Vector2(slotSize, slotSize);
    }
}
