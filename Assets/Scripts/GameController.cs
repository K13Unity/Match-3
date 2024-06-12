using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private ProgressSlotManager _progressSlotManager;
    [SerializeField] private ComboItemManager _comboItemManager;
    [SerializeField] private SlotManager _slotManager;

    [SerializeField] private RectTransform _progressZone;
    [SerializeField] private RectTransform _playZone;
    public RectTransform PlayZone => _playZone;

    [SerializeField] private GameConfig _gameConfig;
    public GameConfig GameConfig => _gameConfig;
    public Vector2 SlotSize { get; set; }
    [SerializeField] private Clock _clock;
    [SerializeField] private ComboProgressBar _comboProgressBar;

    private StateMachine<GameController> _stateMachine;

    private int _heightProgressCount = 15;
    //public const int slotCount = 8;

    //private Vector2 _slotSize;
    private Vector2 _progressSlotSize;
    public InputProcess InputProcess;
    public List<Slot> SlotsToRemoval = new List<Slot>();

    Slot[,] slots;// = new Slot[slotCount, slotCount];
    public Slot[,] Slots
    {
        get { return slots; }
        set { slots = value; }
    }


    void Start()
    {
        _stateMachine = new StateMachine<GameController>(new CreateSlotsState(this));
       // _slotSize = CalculateSlotSize();
       // _slotManager.CreateSlots(slotCount, slotCount, _slotSize);
        _progressSlotSize = CalculateProgressSlotSize();
       // _slotManager.SetMatchChecker(_checkMatches);
        _progressSlotManager.CreateProgressSlots(_heightProgressCount, _progressSlotSize);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) _comboItemManager.CreateComboItem();
        //if (Input.GetKeyDown(KeyCode.C)) _checkMatches.CheckGrid();
        if (Input.GetKeyDown(KeyCode.D)) _clock.AddTime(200);
        if (Input.GetKeyDown(KeyCode.S)) _comboProgressBar.AddComboPoints(400);
        //if (Input.GetKeyDown(KeyCode.A)) _slotManager.RefillSlots();
    }

  /*  private Vector2 CalculateSlotSize()
    {
        Vector2 zoneSize = _playZone.rect.size;
        float slotWidth = zoneSize.x / slotCount;
        float slotHeight = zoneSize.y / slotCount;
        return new Vector2(slotWidth, slotHeight);
    }*/

    private Vector2 CalculateProgressSlotSize()
    {
        Vector2 zoneSize = _progressZone.rect.size;
        float slotSize = Mathf.Min(zoneSize.x, zoneSize.y);
        return new Vector2(slotSize, slotSize);
    }

    public void GetBurnedItemIds(int id)
    {
        _comboItemManager.CreateComboItem(id);
    }
}
