using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGame
{
    public class Game
    {
        private List<int> rolls = new List<int>();

        private const int MAX_PINS_IN_FRAME = 10;
        private const int MAX_FRAMES = 10;
        private const int MAX_ROLLS_IN_FRAME = 2;

        public void Roll(int pins)
        {
            rolls.Add(pins);

        }

        public int Score()
        {
            int score = 0;

            int current = 0;
            for (int frame = 0; frame < MAX_FRAMES; frame++)
            {
                if (IsSpare(current))
                {
                    score += MAX_PINS_IN_FRAME;
                    score += GetSpareBonus(current);

                }
                else
                {
                    score += GetCurrentFramePins(current);
                }

                current += MAX_ROLLS_IN_FRAME;
            }

            return score;
        }

        private int GetCurrentFramePins(int current)
        {
            return rolls.Skip(current).Take(2).Sum();
        }

        private bool IsSpare(int current)
        {
            return GetCurrentFramePins(current) == 10;
        }

        private int GetSpareBonus(int current)
        {
            return rolls.Skip(current + 2).Take(1).Sum();
        }
    }
}
