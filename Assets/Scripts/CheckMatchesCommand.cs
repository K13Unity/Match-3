using System.Collections.Generic;

namespace Assets.Scripts
{
    public static class CheckMatchesCommand
    {
        private static GameController _core;

        public static List<Slot> Execute(GameController core)
        {
            UnityEngine.Debug.Log("+");
            _core = core;
            List<Slot> combinedList = new List<Slot>();
            combinedList.AddRange(CheckHorizontalLines());
            combinedList.AddRange(CheckVerticalLines());
            UnityEngine.Debug.Log(combinedList.Count);
            
            return combinedList;
        }

        private static List<Slot> CheckHorizontalLines()
        {
            for (int row = 0; row < _core.GameConfig.SlotsCount; row++)
            {
                int consecutiveCount = 1;
                List<Slot> toBeBurned = new List<Slot>();

                for (int col = 1; col < _core.GameConfig.SlotsCount; col++)
                {
                    if (_core.Slots[row, col].item.Id == _core.Slots[row, col - 1].item.Id)
                    {
                        if (consecutiveCount == 1)
                        {
                            toBeBurned.Add(_core.Slots[row, col - 1]);
                        }
                        toBeBurned.Add(_core.Slots[row, col]);
                        consecutiveCount++;
                    }
                    else
                    {
                        if (toBeBurned.Count >= 3)
                        {
                            return toBeBurned;
                        }
                        consecutiveCount = 1;
                        toBeBurned.Clear();
                    }
                }
            }
            return new List<Slot>();
        }

        private static List<Slot> CheckVerticalLines()
        {
            for (int col = 0; col < _core.GameConfig.SlotsCount; col++)
            {
                int consecutiveCount = 1;
                List<Slot> toBeBurned = new List<Slot>();

                for (int row = 1; row < _core.GameConfig.SlotsCount; row++)
                {
                    if (_core.Slots[row, col].item.Id == _core.Slots[row - 1, col].item.Id)
                    {
                        if (consecutiveCount == 1)
                        {
                            toBeBurned.Add(_core.Slots[row - 1, col]);
                        }
                        toBeBurned.Add(_core.Slots[row, col]);
                        consecutiveCount++;
                    }
                    else
                    {
                        if (toBeBurned.Count >= 3)
                        {
                            return toBeBurned;
                        }
                        consecutiveCount = 1;
                        toBeBurned.Clear();
                    }
                }
            }
            return new List<Slot>();
        }
    }
}
