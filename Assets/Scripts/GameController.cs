using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ProgressSlotManager _progressSlotManager;
    [SerializeField] private ComboItemManager _comboItemManager;
    [SerializeField] private CheckMatches _checkMatches;
    [SerializeField] private SlotManager _slotManager;

    [SerializeField] private RectTransform _progressZone;
    [SerializeField] private RectTransform _playZone;

    [SerializeField] private Clock _clock;
    [SerializeField] private ComboProgressBar _comboProgressBar;


    private int _heightProgressCount = 15;
    public const int slotCount = 8;

    private Vector2 _slotSize;
    private Vector2 _progressSlotSize;


    void Start()
    {
        _slotSize = CalculateSlotSize();
        _slotManager.CreateSlots(slotCount, slotCount, _slotSize);
        _progressSlotSize = CalculateProgressSlotSize();
        _slotManager.SetMatchChecker(_checkMatches);
        _progressSlotManager.CreateProgressSlots(_heightProgressCount, _progressSlotSize);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _comboItemManager.CreateComboItem();
        if (Input.GetKeyDown(KeyCode.C)) _checkMatches.CheckGrid();
        if (Input.GetKeyDown(KeyCode.D)) _clock.AddTime(200);
        if (Input.GetKeyDown(KeyCode.S)) _comboProgressBar.AddComboPoints(400);
        if (Input.GetKeyDown(KeyCode.A)) _slotManager.RefillSlots();
    }

    private Vector2 CalculateSlotSize()
    {
        Vector2 zoneSize = _playZone.rect.size;
        float slotWidth = zoneSize.x / slotCount;
        float slotHeight = zoneSize.y / slotCount;
        return new Vector2(slotWidth, slotHeight);
    }

    private Vector2 CalculateProgressSlotSize()
    {
        Vector2 zoneSize = _progressZone.rect.size;
        float slotSize = Mathf.Min(zoneSize.x, zoneSize.y);
        return new Vector2(slotSize, slotSize);
    }
}
