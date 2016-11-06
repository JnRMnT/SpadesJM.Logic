using Common;
using Common.Enums;
using Game.Actors;
using Game.Logic;
using Game.Structure;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    /// <summary>
    /// Hand to play. (Respresents 4 moves)
    /// </summary>
    public class Hand
    {
        protected Player initiater;
        protected Turn[] playedCards;
        protected int playedCardCount;
        protected Turn mostValueableCard;
        protected CardType currentTrump;
        protected Round currentRound;

        public Hand(Player initiater, CardType currentTrump,Round currentRound)
        {
            this.initiater = initiater;
            this.currentTrump = currentTrump;
            this.currentRound = currentRound;
            playedCardCount = 0;
            playedCards = new Turn[4];

            mostValueableCard = null;
        }

        public Turn[] GetPlayedCards()
        {
            return playedCards;
        }

        public int GetPlayedCardCount()
        {
            return playedCardCount;
        }

        public CardType GetHandType()
        {
            return playedCards[0].GetPlayedCard().GetCardType();
        }

        public Turn GetMostValuableCard()
        {
            return this.mostValueableCard;
        }

        public Round GetCurrentRoundReference()
        {
            return this.currentRound;
        }

        public virtual void Advance(Turn playedCard){

            if (!DeckHelper.IsValidMove(this, playedCard.GetPlayer().GetPlayersDeck(),playedCard.GetPlayedCard(),playedCard.GetPlayer()))
            {
                playedCard.GetPlayer().AskForAction(ActionType.PLAY_CARD, this,Common.Infos.InfoDescription.CannotPlayThatCard);
                return;
            }

            playedCard.GetPlayer().GetPlayersDeck().UseCard(playedCard.GetPlayedCard());
            playedCards[playedCardCount] = playedCard;
            PlayedCards.AddPlayedCard(playedCard.GetPlayedCard());
            playedCardCount++;

            if (
                mostValueableCard==null ||
                (playedCard.GetPlayedCard().GetCardType() == currentTrump && playedCard.GetPlayedCard().CalculateRealValue(currentTrump) > mostValueableCard.GetPlayedCard().CalculateRealValue(currentTrump)) ||
                (GetHandType() == playedCard.GetPlayedCard().GetCardType() && playedCard.GetPlayedCard().CalculateRealValue(currentTrump) > mostValueableCard.GetPlayedCard().CalculateRealValue(currentTrump))
                )
            {
                mostValueableCard = playedCard;
            }

            if (playedCard.GetPlayedCard().GetCardType() == currentTrump)
            {
                currentRound.SetTrumpUsable(true);
            }

            if (playedCardCount >= 4)
            {
                endHand();
            }else{
                playedCard.GetPlayer().GetNextPlayer().AskForAction(ActionType.PLAY_CARD,this);
            }
        }

        public void StartHand()
        {
            initiater.AskForAction(ActionType.PLAY_CARD, this,Common.Infos.InfoDescription.NoError);
        }

        private void endHand()
        {
            currentRound.HandEnded(mostValueableCard.GetPlayer());
        }

        public Player GetInitiater()
        {
            return initiater;
        }

    }
}
