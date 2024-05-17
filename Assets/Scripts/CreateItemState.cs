using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class CreateItemState : State<GameController>
    {
        private Vector2 _slotSize;
        public CreateItemState(GameController core, Vector2 slotSize) : base(core) 
        {
            _slotSize = slotSize;
        }


        public override void OnEnter()
        {
            for(int row = 0; row < _core.GameConfig.SlotsCount; row++)
            {
                for (int col = 0; col < _core.GameConfig.SlotsCount; col++)
                {
                    CreateItem(_core.Slots[row, col]);
                }
            }
            ChangeState(new InputState(_core));

        }

        private void CreateItem(Slot slot)
        {
            int id;
            bool matchFound;
            do
            {
                matchFound = false;
                
                id = UnityEngine.Random.Range(0, _core.GameConfig.ItemsSpritsConfig.ItemsSprites.Count);

                if (slot.xPos >= 2)
                {
                    if (_core.Slots[slot.yPos, slot.xPos - 1].item.Id == id &&
                        _core.Slots[slot.yPos, slot.xPos - 2].item.Id == id)
                    {
                        matchFound = true;
                    }
                }

                if (slot.yPos >= 2)
                {
                    if (_core.Slots[slot.yPos - 1, slot.xPos].item.Id == id &&
                        _core.Slots[slot.yPos - 2, slot.xPos].item.Id == id)
                    {
                        matchFound = true;
                    }
                }
            } while (matchFound);
            Item item = GameObject.Instantiate(_core.GameConfig.ItemPrefab);
            item.Init(id, _core.GameConfig.ItemsSpritsConfig.ItemsSprites[id], _slotSize);
            item.transform.SetParent(slot.transform);
            item.transform.localPosition = Vector3.zero;
            slot.SetItem(item);
        }
    }
}
