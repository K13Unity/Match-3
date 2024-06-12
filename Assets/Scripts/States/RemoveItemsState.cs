using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class RemoveItemsState : State<GameController>
    {
        public RemoveItemsState(GameController core) : base(core) { }

        public override void OnEnter()
        {
            foreach (var slot in _core.SlotsToRemoval)
            {
                GameObject.Destroy(slot.item.gameObject);
                slot.item = null;
                slot.isBusy = false;
                slot.isBurning = false;
            }

            _core.SlotsToRemoval.Clear();
            
            new FillGapsOnFieldCommand().Execute(_core);
            ChangeState(new InputState(_core));
        }
    }
}
