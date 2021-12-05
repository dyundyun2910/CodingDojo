using System.Collections.Generic;
using System.Linq;

namespace BowlingGame
{
    public static class BowlingSpec
    {
        public const int MAX_PINS_IN_FRAME = 10;
        public const int MAX_FRAMES = 10;
        public const int MAX_ROLLS_IN_FRAME = 2;
        public const int ROLLS_IN_STRIKE = 1;
    }

    public class ScoreCalculator
    {
        private readonly List<int> rolls;

        public ScoreCalculator(List<int> rolls)
        {
            this.rolls = rolls;
        }

        public int Score()
        {
            int score = 0;
            int current = 0;
            for (int frame = 0; frame < BowlingSpec.MAX_FRAMES; frame++)
            {
                score += CalculateFrameScore(current);
                current = CurrentRollAdjuster.Adjust(rolls.Skip(current), current);
            }

            return score;
        }

        private int CalculateFrameScore(int current)
        {
            int score = 0;

            if (BonusJudgement.IsStrike(rolls.Skip(current)))
            {
                score += BowlingSpec.MAX_PINS_IN_FRAME;
                score += BonusServer.GetStrikeBonus(rolls.Skip(current + 1));
            }
            else if (BonusJudgement.IsSpare(rolls.Skip(current)))
            {
                score += BowlingSpec.MAX_PINS_IN_FRAME;
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
    }

    public static class CurrentRollAdjuster
    {
        public static int Adjust(IEnumerable<int> currentRolls, int current)
        {
            int currentRollCount = BonusJudgement.IsStrike(currentRolls) ? BowlingSpec.ROLLS_IN_STRIKE : BowlingSpec.MAX_ROLLS_IN_FRAME;
            return current + currentRollCount;
        }
    }

    public static class BonusJudgement
    {
        public static bool IsSpare(IEnumerable<int> currentRolls)
        {
            return currentRolls.Take(BowlingSpec.MAX_ROLLS_IN_FRAME).Sum() == BowlingSpec.MAX_PINS_IN_FRAME;
        }

        public static bool IsStrike(IEnumerable<int> currentRolls)
        {
            return currentRolls.Take(BowlingSpec.ROLLS_IN_STRIKE).Sum() == BowlingSpec.MAX_PINS_IN_FRAME;
        }
    }

    public static class BonusServer
    {
        public static int GetSpareBonus(IEnumerable<int> nextRolls)
        {
            const int bonusRollsForSpare = 1;
            return nextRolls.Take(bonusRollsForSpare).Sum();
        }

        public static int GetStrikeBonus(IEnumerable<int> nextRolls)
        {
            const int bonusRollsForStrike = 2;
            return nextRolls.Take(bonusRollsForStrike).Sum();
        }
    }
}