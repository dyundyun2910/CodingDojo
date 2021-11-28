using System.Collections.Generic;
using System.Linq;

namespace BowlingGame
{
    public class ScoreCalculator
    {
        private const int MAX_PINS_IN_FRAME = 10;
        private const int MAX_FRAMES = 10;
        private const int MAX_ROLLS_IN_FRAME = 2;
        private const int ROLLS_IN_STRIKE = 1;

        private readonly List<int> rolls;

        public ScoreCalculator(List<int> rolls)
        {
            this.rolls = rolls;
        }

        public int Score()
        {
            int score = 0;
            int current = 0;
            for (int frame = 0; frame < MAX_FRAMES; frame++)
            {
                score += CalculateFrameScore(current);
                current = AdjustCurrentRoll(current);
            }

            return score;
        }

        private int AdjustCurrentRoll(int current)
        {
            if (IsStrike(current))
            {
                current += ROLLS_IN_STRIKE;
            }
            else
            {
                current += MAX_ROLLS_IN_FRAME;
            }

            return current;
        }

        private int CalculateFrameScore(int current)
        {
            int score = 0;

            if (IsStrike(current))
            {
                score += MAX_PINS_IN_FRAME;
                score += BonusServer.GetSpareBonus(rolls.Skip(current + 1));
            }
            else if (IsSpare(current))
            {
                score += MAX_PINS_IN_FRAME;
                score += BonusServer.GetSpareBonus(rolls.Skip(current + 2));

            }
            else
            {
                score += GetCurrentFramePins(current);
            }

            return score;
        }

        private int GetCurrentFramePins(int current)
        {
            return rolls.Skip(current).Take(2).Sum();
        }

        private bool IsSpare(int current)
        {
            return GetCurrentFramePins(current) == MAX_PINS_IN_FRAME;
        }

        private bool IsStrike(int current)
        {
            return rolls.Skip(current).Take(1).Sum() == MAX_PINS_IN_FRAME;
        }
    }

    public static class BonusServer
    {
        public static int GetSpareBonus(IEnumerable<int> nextRolls)
        {
            return nextRolls.Take(1).Sum();
        }

        private static int GetStrikeBonus(IEnumerable<int> nextRolls)
        {
            return nextRolls.Take(2).Sum();
        }

    }
}