using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGame
{
    public class Game
    {
        private List<int> rolls = new List<int>();

        public void Roll(int pins)
        {
            rolls.Add(pins);

        }

        public int Score()
        {
            int score = 0;

            int current = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (rolls.Skip(current).Take(2).Sum() == 10)
                {
                    score += 10;
                    score += rolls.Skip(current + 2).Take(1).Sum();

                }
                else
                {
                    score += rolls.Skip(current).Take(2).Sum();
                }

                current += 2;
            }

            return score;
        }
    }
}
