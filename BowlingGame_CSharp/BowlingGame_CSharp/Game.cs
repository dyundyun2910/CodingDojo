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
            return Calculator.Execute(frames);
        }
    }

    internal class Calculator
    {
        public static int Execute(Frames frames)
        {
            return NormalScoreRule.Calculate(frames);
        }

    }
    internal class NormalScoreRule
    {
        internal static int Calculate(Frames frames)
        {
            var framesList = frames.ToList();
            return framesList.Sum(frame => frame.Score());
        }
    }

    internal class Frames
    {
        private const int MAX_FRAMES = 10;

        private readonly List<Frame> frames = new List<Frame>() { new Frame() };

        internal void Roll(Roll roll)
        {
            if (frames.Last().IsFull())
            {
                if (IsFull()) throw new InvalidOperationException();
                frames.Add(new Frame());
            }

            frames.Last().AddRoll(roll);
        }

        private bool IsFull()
        {
            return frames.Count >= MAX_FRAMES;
        }

        internal List<Frame> ToList()
        {
            return new List<Frame>(frames);
        }
    }


    internal class Frame
    {
        private const int MAX_ROLLS = 2;

        private Rolls rolls = new Rolls();

        internal void AddRoll(Roll roll)
        {
            if (IsFull()) throw new InvalidOperationException();
            rolls = rolls.Add(roll);
        }

        internal bool IsFull()
        {
            return rolls.Count() >= MAX_ROLLS;
        }

        internal int Score()
        {
            return rolls.Sum();
        }
    }

    internal class Rolls
    {
        private readonly List<Roll> rolls;

        internal Rolls() { rolls = new List<Roll>(); }
        internal Rolls(List<Roll> rolls) { this.rolls = rolls; }

        internal Rolls Add(Roll roll)
        {
            return new Rolls(
                new List<Roll>(this.rolls){ roll }
                );
        }

        internal int Sum()
        {
            return rolls.Sum(roll => roll.ToInt());
        }

        internal int Count()
        {
            return rolls.Count();
        }
    }

    internal class Roll
    {
        private const int MAX_PIN = 10;
        private const int MIN_PIN = 0;

        private readonly int pins;

        internal Roll(int pins)
        {
            if (pins < MIN_PIN) throw new ArgumentOutOfRangeException();
            if (pins > MAX_PIN) throw new ArgumentOutOfRangeException();

            this.pins = pins;
        }

        internal int ToInt()
        {
            return pins;
        }
    }

}
