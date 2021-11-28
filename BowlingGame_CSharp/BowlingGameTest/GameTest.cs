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

        private void MultiRollBySamePins(int knockedDownPins, int rollCount)
        {
            for (int i = 0; i < rollCount; i++)
            {
                game.Roll(knockedDownPins);
            }
        }
    }
}
