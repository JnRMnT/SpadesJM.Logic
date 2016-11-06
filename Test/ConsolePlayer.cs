using AI;
using Common.Enums;
using Common.Infos;
using Game;
using Game.Actors;
using Game.Logic;
using Game.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    class ConsolePlayer : Computer
    {
        public ConsolePlayer(GameTable gameTable)
            : base(gameTable)
        {

        }

        public override void AskForAction(ActionType actionType, object callbackObject, InfoDescription error)
        {
            this.callbackObject = callbackObject;
            switch (actionType)
            {
                case ActionType.DO_BIDDING:
                    Bid(((InitialPhase)callbackObject).GetLowestPossibleBid());
                    break;
                case ActionType.PLAY_CARD:
                    Hand currentHand = (Hand)callbackObject;
                    if (DeckHelper.GetAvailableMoves(currentHand, playersDeck, this).Count == 0)
                    {
                        return;
                    }
                    PlayCard(DeckHelper.GetAvailableMoves(currentHand, playersDeck, this)[0]);
                    break;
                case ActionType.SET_TRUMP_TYPE:
                    SetTrumpType(CardType.Spade);
                    break;
            }
        }
    }
}