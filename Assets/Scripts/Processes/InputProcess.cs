using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class InputProcess : Process
    {
        public Action onMatches;

        private GameController _core;
        public InputProcess(GameController core) : base(core)
        {
            _core = core;
        }

        protected override void OnStart()
        {
            foreach (var slot in _core.Slots)
            {
                slot.onHorisontal += OnHorizontalSwap;
                slot.onVertical += OnVerticalSwap;
            }
        }

        private void OnHorizontalSwap(Slot slot, int direction)
        {
            var x = slot.xPos + direction;
            if (x < 0 || x >= _core.GameConfig.SlotsCount) return;
            var targetSlot = _core.Slots[slot.yPos, x];
            if (slot.isBusy || targetSlot.isBusy) return;
            if (slot.isBurning || targetSlot.isBurning) return;
            
            new MoveItemCommand().Execute(slot, targetSlot, () =>
            {
                OnMoveCompleted(slot, targetSlot);
            });
        }

        private void OnVerticalSwap(Slot slot, int direction)
        {
            var y = slot.yPos + direction;
            if (y < 0 || y >= _core.GameConfig.SlotsCount) return;
            var targetSlot = _core.Slots[y, slot.xPos];
            if (slot.isBusy || targetSlot.isBusy) return;
            if (slot.isBurning || targetSlot.isBurning) return;
            
            new MoveItemCommand().Execute(slot, targetSlot, () =>
            {
                OnMoveCompleted(slot, targetSlot);
            });
        }

        private void OnMoveCompleted(Slot fromSlot, Slot toSlot)
        {
            _core.StartCoroutine(WaitForEndOfFrame(() =>
            {
                var result = CheckMatchesCommand.Execute(_core);
                if (result.Count > 0)
                {
                    result.ForEach(n => n.isBurning = true);
                    _core.SlotsToRemoval = result;
                    onMatches?.Invoke();
                }
            }));
        }
        
        private IEnumerator WaitForEndOfFrame(Action action)
        {
            yield return new WaitForEndOfFrame();
            action?.Invoke(); 
        }
        
        protected override void OnStop()
        {
            foreach (var slot in _core.Slots)
            {
                slot.onHorisontal -= OnHorizontalSwap;
                slot.onVertical -= OnVerticalSwap;
            }
        }
    }
}
