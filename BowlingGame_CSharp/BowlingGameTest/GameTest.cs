using Microsoft.VisualStudio.TestTools.UnitTesting;
using BowlingGame;
using System;

namespace BowlingGameTest
{
    [TestClass]
    public class GameTest
    {
        private Game g;

        [TestInitialize]
        public void Setup()
        {
            g = new Game();
        }


        [TestMethod]
        public void TestOneRoll()
        {
            g.Roll(3);
            Assert.AreEqual(3, g.Score());
        }

        [TestMethod]
        public void TestOneRoll2()
        {
            g.Roll(2);
            Assert.AreEqual(2, g.Score());
        }

        [TestMethod]
        public void TestTwoRoll()
        {
            g.Roll(2);
            g.Roll(3);
            Assert.AreEqual(5, g.Score());
        }

        [TestMethod]
        public void TestOneRollOutOfRange()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => g.Roll(-1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => g.Roll(11));
        }

        [TestMethod]
        public void TestNormalRollCountIs20()
        {
            ManyRolls(rollCount: 20, pins: 0);

            Assert.ThrowsException<InvalidOperationException>(() => g.Roll(1));
        }

        [TestMethod]
        public void TestGutterGame()
        {
            ManyRolls(rollCount: 20, pins: 0);

            Assert.AreEqual(0, g.Score());
        }

        [TestMethod]
        public void TestAllOnes()
        {
            ManyRolls(rollCount: 20, pins: 1);

            Assert.AreEqual(20, g.Score());
        }

        [TestMethod]
        public void TestOneSpare()
        {
            g.Roll(5);
            g.Roll(5);  //Spare
            g.Roll(3);
            ManyRolls(rollCount: 17, pins: 0);

            Assert.AreEqual(16, g.Score());
        }

        [TestMethod]
        public void TestOneStrike()
        {
            g.Roll(10); //Strike
            g.Roll(3);
            g.Roll(4);
            ManyRolls(rollCount: 16, pins: 0);

            Assert.AreEqual(24, g.Score());
        }

        [TestMethod]
        public void TestPerfectGame()
        {
            ManyRolls(rollCount: 12, pins: 10);

            Assert.AreEqual(300, g.Score());
        }

        [TestMethod]
        public void TestTenFrameTurkey()
        {
            ManyRolls(rollCount: 18, pins: 0);
            ManyRolls(rollCount: 3, pins: 10);

            Assert.AreEqual(30, g.Score());
        }


        [TestMethod]
        public void TestTenFrameSpare()
        {
            ManyRolls(rollCount: 18, pins: 0);
            g.Roll(5);
            g.Roll(5);  //Spare
            g.Roll(3);

            Assert.AreEqual(13, g.Score());
        }

        private void ManyRolls(int rollCount, int pins)
        {
            for (int i = 0; i < rollCount; i++)
            {
                g.Roll(pins);
            }
        }
    }
}
