using Common;
using Common.Enums;
using Common.Structure;
using Game.Actors;
using Game.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class ConsoleGameTable : GameTable
    {
        public ConsoleGameTable()
            : base()
        {

        }

        public override void InitializeGame()
        {
            GameEndingCondition gameEndingCondition = null;
            switch (gameMode)
            {
                case GameMode.TargetScore:
                    gameEndingCondition = new TargetScoreCondition(modeParams);
                    break;
                case GameMode.RoundCount:
                    object[] roundCountConditionParams = new object[modeParams.Length + 1];
                    roundCountConditionParams[0] = game;
                    modeParams.CopyTo(roundCountConditionParams, 1);
                    gameEndingCondition = new RoundCountCondition(modeParams);
                    break;
            }

            //      HANDLE PLAYER ACTIONS   //

            Deck mainDeck = new Deck();
            List<Card>[] splittedCards = mainDeck.Deal();

            for (int i = 0; i < 4; i++)
            {
                Deck newPlayerDeck = new Deck(splittedCards[i]);
                seats[i].SetPlayersDeck(newPlayerDeck);
            }

            game = new ConsoleGame(this, gameEndingCondition);
            game.Commence();
            state = GameState.PLAYING;
        }

    }
}
