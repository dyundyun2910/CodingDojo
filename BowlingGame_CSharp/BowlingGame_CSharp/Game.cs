using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGame
{
    public class Game
    {
        private List<Roll> rolls;

        public Game()
        {
            rolls = new List<Roll>();
        }

        public void Roll(int pins)
        {
            rolls.Add(new Roll(pins));
        }

        public int Score()
        {
            int score = 0;
            rolls.ForEach(roll => score += roll.ToInt());
            return score;
        }
    }

    internal class Roll
    {
        const int MAX_PIN = 10;
        const int MIN_PIN = 0;

        private int pins;

        public Roll(int pins)
        {
            if (pins < MIN_PIN) throw new ArgumentOutOfRangeException();
            if (pins > MAX_PIN) throw new ArgumentOutOfRangeException();

            this.pins = pins;
        }

        public int ToInt()
        {
            return pins;
        }
    }

}
