using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class RefillProcess : Process
    {
        private GameController _core;
        public RefillProcess(GameController core) : base(core)
        {
            _core = core;
        }

        protected override void OnStart()
        {
            RefillSlots();
        }


        public void RefillSlots()
        {
            for (int x = 0; x < _core.GameConfig.SlotsCount; x++)
            {
                int emptyCount = 0;
                for (int y = 0; y < _core.GameConfig.SlotsCount; y++)
                {
                    if (_core.Slots[y, x].item == null)
                    {
                        emptyCount++;
                    }
                    else if (emptyCount > 0)
                    {
                        _core.Slots[y, x].item.Move(_core.Slots[y - emptyCount, x]);
                        y -= emptyCount;
                        emptyCount = 0;
                    }
                }

                for (int y = 0; y < emptyCount; y++)
                {
                    // AddingNewItems(_core.Slots[_core.GameConfig.SlotsCount - 1 - y, x]);
                }
            }
        }
    }


}
