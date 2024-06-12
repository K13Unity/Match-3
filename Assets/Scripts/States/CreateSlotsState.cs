using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CreateSlotsState: State<GameController>
    {
        public CreateSlotsState(GameController core) : base(core) { }

        private Vector2 _slotSize;

        public override void OnEnter()
        {
            _slotSize = CalculateSlotSize();
            _core.SlotSize = _slotSize;
            CreateSlots(_core.GameConfig.SlotsCount, _core.GameConfig.SlotsCount, _slotSize);
            ChangeState(new CreateItemState(_core, _slotSize));
        }

        private Vector2 CalculateSlotSize()
        {
            Vector2 zoneSize = _core.PlayZone.rect.size;
            float slotWidth = zoneSize.x / _core.GameConfig.SlotsCount;
            float slotHeight = zoneSize.y / _core.GameConfig.SlotsCount;
            return new Vector2(slotWidth, slotHeight);
        }

        public void CreateSlots(int rowCount, int colCount, Vector2 slotSize)
        {
            _core.Slots = new Slot[rowCount, colCount];
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    Slot slot = GameObject.Instantiate(_core.GameConfig.SlotPrefab, _core.PlayZone);
                    float xPos = col * slotSize.x + slotSize.x / 2 - _core.PlayZone.rect.size.x / 2;
                    float yPos = row * slotSize.y + slotSize.y / 2 - _core.PlayZone.rect.size.y / 2;
                    slot.Init(col, row, new Vector2(slotSize.x, slotSize.y));
                    slot.transform.localPosition = new Vector2(xPos, yPos);
                    _core.Slots[row, col] = slot;
                }
            }
        }
    }
}
