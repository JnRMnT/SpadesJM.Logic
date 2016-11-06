using System.Collections.Generic;
using System.Text;
using Game.Actors;
using Common.Infos;
using Common.Enums;
using Game.Structure;
using Game;
using Game.Logic;
using System.Threading;

namespace AI
{
    public class Computer : Player
    {
        public Computer(GameTable gameTable)
            : base(gameTable)
        {

        }

        public override void AskForAction(ActionType actionType, object callbackObject, InfoDescription error)
        {
            this.callbackObject = callbackObject;
            switch (actionType)
            {
                case ActionType.DO_BIDDING:
                    HandleBidding();
                    break;
                case ActionType.PLAY_CARD:
                    Hand currentHand = (Hand)callbackObject;
                    PlayCard(DeckHelper.GetBestMove(currentHand, playersDeck, this));
                    break;
                case ActionType.SET_TRUMP_TYPE:
                    SetTrumpType(GetPlayersDeck().GetTrumpToChoose());
                    break;
            }
        }

        protected void HandleBidding()
        {
            int lowestPossibleBid = ((InitialPhase)callbackObject).GetLowestPossibleBid();

            if (System.Math.Round(GetPlayersDeck().GetDeckValue()) >= lowestPossibleBid)
            {
                Bid((int)System.Math.Round(GetPlayersDeck().GetDeckValue()));
            }
            else if ((int)System.Math.Ceiling(GetPlayersDeck().GetDeckValue() + 1) >= lowestPossibleBid)
            {
                Bid((int)System.Math.Ceiling(GetPlayersDeck().GetDeckValue() + 1));
            }
            else {
                Bid(-1);
            }

        }
    }
}
