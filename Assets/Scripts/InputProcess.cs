using System;
using System.Diagnostics;
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
            slot.item.Move(targetSlot);
            targetSlot.item.Move(slot, () =>
            {
                var result = CheckMatchesCommand.Execute(_core);
                if (result.Count > 0)
                {
                    _core.SlotsToRemoval = result;
                    onMatches?.Invoke();
                }
            });
        }

        private void OnVerticalSwap(Slot slot, int direction)
        {
            var y = slot.yPos + direction;
            if (y < 0 || y >= _core.GameConfig.SlotsCount) return;
            var targetSlot = _core.Slots[y, slot.xPos];
            slot.item.Move(targetSlot);
            targetSlot.item.Move(slot, () =>
            {
                var result = CheckMatchesCommand.Execute(_core);
                if (result.Count > 0)
                {
                    _core.SlotsToRemoval = result;
                    onMatches?.Invoke();
                }
            });
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
