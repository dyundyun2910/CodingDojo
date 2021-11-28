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
    }
}
