using UnityEngine;

namespace Assets.Scripts
{
    public class FillGapsOnFieldCommand
    {
        private int _movingCount;
        private GameController _core;
        
        public void Execute(GameController core)
        {
            _core = core;
            _movingCount = 0;
            for (int x = 0; x < _core.GameConfig.SlotsCount; x++)
            {
                CheckColumn(x);
            }
        }

        private bool CheckColumn(int column)
        {
            for (int y = 1; y < _core.GameConfig.SlotsCount; y++)
            {
                Slot slot = _core.Slots[y, column];

                if (slot.isBurning) continue;
                if (slot.item == default) continue;
                
                Slot lowerSlot = _core.Slots[y - 1, column];
                if (lowerSlot.item == default)
                {
                    _movingCount++;
                    new MoveItemCommand().Execute(slot, lowerSlot, () =>
                    {
                        OnMoveCompleted(slot, lowerSlot);
                    });
                }
            }
            if(_movingCount >0) return true;
            return false;
        }
        
        private void OnMoveCompleted(Slot fromSlot, Slot toSlot)
        {
            _movingCount--;

            if (_movingCount == 0)
            {
                if (CheckColumn(toSlot.xPos) == false)
                {
                    FillEmptySlotsWithNewItems();
                    CheckForMatches();
                }
            }
        }

        private void FillEmptySlotsWithNewItems()
        {
            foreach (var slot in _core.Slots)
            {
                if (slot.item == null) CreateAndPlaceNewItemInSlot(slot);
            }
        }

        private void CreateAndPlaceNewItemInSlot(Slot slot)
        {
            int id = Random.Range(0, _core.GameConfig.ItemsSpritsConfig.ItemsSprites.Count);
            Item item = GameObject.Instantiate(_core.GameConfig.ItemPrefab);
            item.Init(id, _core.GameConfig.ItemsSpritsConfig.ItemsSprites[id], _core.SlotSize);
            item.transform.SetParent(slot.transform);
            item.transform.localPosition = Vector3.zero;
            slot.SetItem(item);
        }

        private void CheckForMatches()
        {
            var matches = CheckMatchesCommand.Execute(_core);
            if (matches.Count > 0)
            {
                matches.ForEach(slot => slot.isBurning = true);
                _core.SlotsToRemoval = matches;
                _core.InputProcess.onMatches?.Invoke();
            }
        }
    }
}