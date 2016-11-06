using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace UnitTests
{
    public class TestHelper
    {
        public static ConsoleGameTable CreateTestGame(GameMode gameMode, object endingCondition)
        {
            ConsoleGameTable gameTable = new ConsoleGameTable();
            object[] endingConditionParams = new object[2];
            endingConditionParams[0] = gameTable;
            endingConditionParams[1] = endingCondition;
            gameTable.ChangeGameMode(gameMode, endingConditionParams);

            ConsolePlayer player1 = new ConsolePlayer(gameTable);
            ConsolePlayer player2 = new ConsolePlayer(gameTable);
            ConsolePlayer player3 = new ConsolePlayer(gameTable);
            ConsolePlayer player4 = new ConsolePlayer(gameTable);

            gameTable.Sit(player1);
            gameTable.Sit(player2);
            gameTable.Sit(player3);
            gameTable.Sit(player4);

            return gameTable;
        }
    }
}
