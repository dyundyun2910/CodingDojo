using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingGame;

namespace BowlingGameTest
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void TestCreate()
        {
            new Game();
        }

        [TestMethod]
        public void TestOneRoll()
        {
            var g = new Game();
            g.Roll(3);
            Assert.AreEqual(3, g.Score());
        }

        [TestMethod]
        public void TestOneRoll2()
        {
            var g = new Game();
            g.Roll(2);
            Assert.AreEqual(2, g.Score());
        }
    }
}
