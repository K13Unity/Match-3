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
                _core.SlotsToRemoval.Clear();
            }
            ChangeState(new InputState(_core));
        }
    }
}
