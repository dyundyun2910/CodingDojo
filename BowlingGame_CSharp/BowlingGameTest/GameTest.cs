using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingGame;

namespace BowlingGameTest
{
    [TestClass]
    public class GameTest
    {
        private Game game;

        [TestInitialize]
        public void SetUp()
        {
            game = new Game();
        }

        [TestMethod]
        public void TestOneRoll()
        {
            game.Roll(3);
            Assert.AreEqual(3, game.Score());
        }

        [TestMethod]
        public void TestOneRoll2()
        {
            game.Roll(2);
            Assert.AreEqual(2, game.Score());
        }

        [TestMethod]
        public void TestGutterGame()
        {
            MultiRollBySamePins(knockedDownPins: 0, rollCount: 20);

            Assert.AreEqual(0, game.Score());
        }

        [TestMethod]
        public void TestAllOnes()
        {
            MultiRollBySamePins(knockedDownPins: 1, rollCount: 20);

            Assert.AreEqual(20, game.Score());
        }

        [TestMethod]
        public void TestOneSpare()
        {
            game.Roll(5);
            game.Roll(5);   // Spare

            game.Roll(3);
            MultiRollBySamePins(knockedDownPins: 0, rollCount: 17);

            Assert.AreEqual(16, game.Score());
        }

        [TestMethod]
        public void TestOneStrike()
        {
            game.Roll(10);  // Strike

            game.Roll(3);
            game.Roll(4);

            MultiRollBySamePins(knockedDownPins: 0, rollCount: 16);

            Assert.AreEqual(24, game.Score());
        }

        private void MultiRollBySamePins(int knockedDownPins, int rollCount)
        {
            for (int i = 0; i < rollCount; i++)
            {
                game.Roll(knockedDownPins);
            }
        }
    }
}
