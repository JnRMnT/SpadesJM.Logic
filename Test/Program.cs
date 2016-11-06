using Game.Actors;
using Game.Logic;
using Game.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleGameTable gameTable = new ConsoleGameTable();
            object[] endingConditionParams = new object[2];
            endingConditionParams[0] = gameTable;
            endingConditionParams[1] = (object)5;
            gameTable.ChangeGameMode(Common.Enums.GameMode.RoundCount, endingConditionParams);

            ConsolePlayer player1 = new ConsolePlayer(gameTable);
            ConsolePlayer player2 = new ConsolePlayer(gameTable);
            ConsolePlayer player3 = new ConsolePlayer(gameTable);
            ConsolePlayer player4 = new ConsolePlayer(gameTable);

            gameTable.Sit(player1);
            gameTable.Sit(player2);
            gameTable.Sit(player3);
            gameTable.Sit(player4);

            gameTable.InitializeGame();

            while (!((ConsoleGame)gameTable.GetGameInstance()).gameEnded)
            {

            }

            Console.ReadLine();
        }
    }
}
