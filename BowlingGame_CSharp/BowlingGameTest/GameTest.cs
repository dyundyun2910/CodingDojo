﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            for (int i = 0; i < 20; i++)
            {
                g.Roll(0);
            }

            Assert.ThrowsException<InvalidOperationException>(() => g.Roll(1));
        }
    }
}
