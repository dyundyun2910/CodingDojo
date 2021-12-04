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
            var rules = new List<IScoreRule>() { 
                new NormalScoreRule(),
                new BonusScoreRule()
            };
            return rules.Sum(rule => rule.Calculate(frames));
        }
    }

    interface IScoreRule
    {
        int Calculate(Frames frames);
    }

    internal class NormalScoreRule : IScoreRule
    {
        public int Calculate(Frames frames)
        {
            var framesList = frames.ToList();
            return framesList.Sum(frame => frame.Score());
        }
    }

    internal class BonusScoreRule : IScoreRule
    {
        public int Calculate(Frames frames)
        {
            var framesList = frames.ToList();
            return framesList.Sum(frame => CalculateFrame(frame, frames));
        }

        private int CalculateFrame(Frame frame, Frames frames)
        {
            Rolls restRolls = frames.GetRestRolls(frame);

            const int spareBonusCount = 1;
            var bonusCount = IsSpare(frame) ? spareBonusCount : 0;

            return restRolls.ToList().Take(bonusCount).Sum(roll => roll.ToInt());
        }

        private bool IsSpare(Frame frame)
        {
            return frame.Score() == Roll.MAX_PIN;
        }
    }

    internal class Frames
    {
        private const int MAX_FRAMES = 10;

        private readonly List<Frame> frames = new List<Frame>() { new Frame() };

        internal void Roll(Roll roll)
        {
            var lastFrame = frames.Last();
            if (lastFrame.IsFull())
            {
                if (IsFull()) throw new InvalidOperationException();

                frames.Add(new Frame());
                lastFrame = frames.Last();
            }

            lastFrame.AddRoll(roll);
        }

        private bool IsFull()
        {
            return frames.Count >= MAX_FRAMES;
        }

        internal List<Frame> ToList()
        {
            return new List<Frame>(frames);
        }

        internal Rolls GetRestRolls(Frame currentFrame)
        {
            var restFrames = frames.Where(frame => frames.IndexOf(frame) > frames.IndexOf(currentFrame)).ToList();

            var rolls = new List<Roll>();
            restFrames.ForEach(frame =>
            {
                rolls.AddRange((frame.ToListRolls()));
            });

            return new Rolls(rolls);
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

        internal List<Roll> ToListRolls()
        {
            return new List<Roll>(rolls.ToList());
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

        internal List<Roll> ToList()
        {
            return rolls.ToList();
        }
    }

    internal class Roll
    {
        internal const int MAX_PIN = 10;
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
