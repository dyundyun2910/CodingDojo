using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGame
{
    public class Game
    {
        private readonly List<int> rolls = new List<int>();

        public void Roll(int pins)
        {
            rolls.Add(pins);

        }

        public int Score()
        {
            return new ScoreCalculator(rolls).Score();
        }
    }
}