using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingGame
{
    public class Game
    {
        private readonly Frames frames = new Frames();

        public void Roll(int pins)
        {
            frames.Roll(new Roll(pins));
        }

        public int Score()
        {
            return frames.Score();
        }
    }

    internal class Frames
    {
        private List<Frame> frames = new List<Frame>() { new Frame() };

        public void Roll(Roll roll)
        {
            if (frames.Last().IsFull())
            {
                frames.Add(new Frame());
            }
            frames.Last().AddRoll(roll);
        }

        public int Score()
        {
            int score = 0;
            frames.ForEach(frame => score += frame.Score());
            return score;
        }
    }


    internal class Frame
    {
        private const int MAX_ROLLS = 2;

        private List<Roll> rolls = new List<Roll>();

        public void AddRoll(Roll roll)
        {
            if (IsFull()) throw new InvalidOperationException();
            rolls.Add(roll);
        }

        public bool IsFull()
        {
            return rolls.Count >= MAX_ROLLS;
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
        private const int MAX_PIN = 10;
        private const int MIN_PIN = 0;

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
