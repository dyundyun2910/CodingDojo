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
            var bonusCount = BonusRule.GetBonusCount(frame);

            return restRolls.TakeSum(bonusCount);
        }
    }

    internal class BonusRule
    {
        static List<IBonusRule> rules = new List<IBonusRule>(){
                new SpareRule(),
                new StrikeRule(),
                new FinalFrameRule()
            };

        internal static int GetBonusCount(Frame frame)
        {
            if (frame.IsFinalFrame()) return 0;
            return rules.Sum(rule => rule.GetBonusCount(frame));
        }

        internal static bool IsAnyBonus(Frame frame)
        {
            return rules.Any(rule => rule.IsSatisfiedBy(frame));
        }
    }

    interface IBonusRule
    {
        int GetBonusCount(Frame frame);
        bool IsSatisfiedBy(Frame frame);
    }

    internal class StrikeRule : IBonusRule
    {
        private const int bonusCount = 2;

        public bool IsSatisfiedBy(Frame frame)
        {
            return frame.Score() == Roll.MAX_PIN
                && frame.ToListRolls().Count == 1;
        }

        public int GetBonusCount(Frame frame)
        {
            if (IsSatisfiedBy(frame)) return bonusCount;
            return 0;
        }
    }

    internal class SpareRule : IBonusRule
    {
        private const int bonusCount = 1;

        public bool IsSatisfiedBy(Frame frame)
        {
            return frame.Score() == Roll.MAX_PIN
                && frame.ToListRolls().Count == Frame.MAX_ROLLS;
        }

        public int GetBonusCount(Frame frame)
        {
            if (IsSatisfiedBy(frame)) return bonusCount;
            return 0;
        }
    }

    internal class FinalFrameRule : IBonusRule
    {
        private const int bonusCount = 0;

        public bool IsSatisfiedBy(Frame frame)
        {
            if (!frame.IsFinalFrame()) return false;

            return frame.ToListRolls().Take(2).Sum(roll => roll.ToInt()) >= Roll.MAX_PIN;
        }

        public int GetBonusCount(Frame frame)
        {
            return bonusCount;
        }
    }

    internal class Frames
    {
        private const int MAX_FRAMES = 10;

        private readonly List<Frame> frames = new List<Frame>() { new NormalFrame() };

        internal void Roll(Roll roll)
        {
            if (IsGameFull()) throw new InvalidOperationException();

            var lastFrame = frames.Last();
            if (lastFrame.IsFull())
            {
                AddFrame();
                lastFrame = frames.Last();
            }

            lastFrame.AddRoll(roll);
        }

        private bool IsGameFull()
        {
            if (frames.Count < MAX_FRAMES) return false;

            var lastFrame = frames.Last();
            return lastFrame.IsFull();
        }

        private void AddFrame()
        {
            if (IsNextFinalFrame())
            {
                frames.Add(new FinalFrame());
                return;
            }

            frames.Add(new NormalFrame());
        }

        private bool IsNextFinalFrame()
        {
            return frames.Count == MAX_FRAMES - 1;
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

    abstract class Frame
    {
        public const int MAX_ROLLS = 2;
        
        protected Rolls rolls = new Rolls();

        abstract internal bool IsFull();
        abstract internal bool IsFinalFrame();

        internal void AddRoll(Roll roll)
        {
            if (IsFull()) throw new InvalidOperationException();
            rolls = rolls.Add(roll);
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

    internal class NormalFrame : Frame
    {

        internal override bool IsFull()
        {
            return rolls.Count() >= Frame.MAX_ROLLS
                || rolls.Sum() == Roll.MAX_PIN;
        }

        internal override bool IsFinalFrame()
        {
            return false;
        }
    }

    internal class FinalFrame : Frame
    {
        private const int MAX_ROLLS_FINAL_BONUS = 3;

        internal override bool IsFull()
        {
            var maxRolls = BonusRule.IsAnyBonus(this) ? MAX_ROLLS_FINAL_BONUS : Frame.MAX_ROLLS;
            return rolls.Count() >= maxRolls;
        }

        internal override bool IsFinalFrame()
        {
            return true;
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

        internal int TakeSum(int takeCount)
        {
            return rolls.Take(takeCount).Sum(roll => roll.ToInt());
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
