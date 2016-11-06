using Common;
using Common.Enums;
using Common.Infos;
using Common.Structure;
using Game.Structure;
using System.Collections.Generic;
using System.Text;

namespace Game.Actors
{
    public class Player
    {
        public string PlayerName;
        protected GameTable gameTable;
        protected object callbackObject;
        protected Deck playersDeck;

        public Player(GameTable gameTable)
        {
            this.gameTable = gameTable;
        }

        public int GetPlayersSeat()
        {
            return gameTable.GetPlayersSeat(this);
        }

        public virtual void AskForAction(ActionType actionType, object callbackObject)
        {
            AskForAction(actionType, callbackObject, InfoDescription.NoError);
        }
        public virtual void AskForAction(ActionType actionType, object callbackObject, InfoDescription error)
        {
            //  DISPLAY ERROR MESSAGE
            this.callbackObject = callbackObject;
        }

        public virtual void PlayCard(Card cardToPlay)
        {
            Turn playedTurn = new Turn(this, cardToPlay);
            ((Hand)callbackObject).Advance(playedTurn);
        }

        public virtual void SetTrumpType(CardType trumpType)
        {
            ((InitialPhase)callbackObject).setTrumpType(trumpType, this);
        }

        public virtual void Bid(int bid)
        {
            ((InitialPhase)callbackObject).Answer(this, bid);
        }


        public Player GetNextPlayer()
        {
            return gameTable.GetPlayerSeatedAt((GetPlayersSeat() + 1) % 4);
        }

        public Player GetPreviousPlayer()
        {
            var seat = (GetPlayersSeat() - 1);
            if (seat < 0) seat += 4;
            return gameTable.GetPlayerSeatedAt(seat);
        }

        public Deck GetPlayersDeck()
        {
            return this.playersDeck;
        }

        public virtual void SetPlayersDeck(Deck newDeck)
        {
            this.playersDeck = newDeck;
        }

        public void UpdateName(string prefix)
        {
            if (this.PlayerName == null)
            {
                this.PlayerName = prefix + (GetPlayersSeat() + 1).ToString();
            }
        }

        public void HandleActionTimeout(ActionType actionType, object callbackObject)
        {
            if (callbackObject == null)
            {
                return;
            }
            switch (actionType)
            {
                case ActionType.PLAY_CARD:
                    Hand currentHand = (Hand)callbackObject;
                    PlayCard(Game.Logic.DeckHelper.GetAvailableMoves(currentHand, playersDeck, this)[0]);
                    break;
                case ActionType.SET_TRUMP_TYPE:
                    SetTrumpType(CardType.Spade);
                    break;
                case ActionType.DO_BIDDING:
                    Bid(-1);
                    break;
            }
        }
    }
}
