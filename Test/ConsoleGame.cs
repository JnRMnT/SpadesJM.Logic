using Game.Actors;
using Game.Logic;
using Game.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class ConsoleGame : GameObject
    {

        public bool gameEnded;

        public ConsoleGame(GameTable gameTable, GameEndingCondition gameEndingCondition)
            : base(gameTable, gameEndingCondition)
        {
            gameEnded = false;
        }

        public override void EndGame()
        {
            Console.WriteLine("Oyun Bitti. Skorlar :");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine("Player " + i + " : " + Game.Actors.ScoreBoard.GetPlayerScore(i));
            }

            gameEnded = true;
        }

    }
}
