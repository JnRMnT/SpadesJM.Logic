using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test;
using Common.Enums;

namespace UnitTests
{
    [TestClass]
    public class GameplayTests
    {
        [TestMethod]
        public void RoundCountGoalTest()
        {
            ConsoleGameTable gameTable = TestHelper.CreateTestGame(GameMode.RoundCount, 5);
            gameTable.InitializeGame();
            Assert.IsTrue(gameTable.GetGameInstance().IsGameConditionMet());
        }

        [TestMethod]
        public void ScoreGoalTest()
        {
            ConsoleGameTable gameTable = TestHelper.CreateTestGame(GameMode.TargetScore, 62);
            gameTable.InitializeGame();
            Assert.IsTrue(gameTable.GetGameInstance().IsGameConditionMet());
        }
    }
}
