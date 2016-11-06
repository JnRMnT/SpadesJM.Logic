using Common.Enums;
using Game.Actors;
using Game.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Structure
{
    /// <summary>
    /// Round represents the part where 13 cards have been played and scores gained.
    /// </summary>
    public class Round
    {
        protected InitialPhase activeBidding;
        protected RoundScore[] roundScores;
        protected CardType trumpType;
        protected Player firstPlayer;
        protected Player bidWinningPlayer;
        protected GameObject gameInstance;
        protected int handCount;
        protected bool trumpUsable;

        public Round(GameObject gameInstance,Player firstPlayer)
        {
            this.gameInstance = gameInstance;
            this.firstPlayer = firstPlayer;
            this.handCount = 1;
            this.trumpUsable = false;

        }

        public virtual void InitiateBidding()
        {
            //  HANDLE BIDDING
            this.activeBidding = new InitialPhase(firstPlayer,this);
            this.activeBidding.StartAsking();
        }

        public bool isTrumpUsable()
        {
            return trumpUsable;
        }

        public CardType GetTrumpType()
        {
            return trumpType;
        }

        public void BiddingCallback(CardType trumpType,Player winner,RoundScore[] roundScores)
        {
            this.trumpType = trumpType;
            this.bidWinningPlayer = winner;
            this.roundScores = roundScores;
            StartRound();
        }

        public virtual void StartRound()
        {
            Hand firstHand = new Hand(bidWinningPlayer,trumpType,this);
            firstHand.StartHand();
        }

        public void EndRound(){
            gameInstance.GetGameTableRefrence().DealCards();
            PlayedCards.ResetPlayedCards();
            gameInstance.GetGameTableRefrence().UpdatePlayerDeckValues();
            gameInstance.RoundEnded(firstPlayer);
        }
        
        public virtual void HandEnded(Player winner)
        {
            roundScores[winner.GetPlayersSeat()].IncrementGot();
            handCount++;
            if (handCount > 13)
            {
                EndRound();
            }
            else
            {
                Hand nextHand = new Hand(winner, trumpType, this);
                nextHand.StartHand();
            }
        }

        public GameObject GetGameObjectReference()
        {
            return gameInstance;
        }

        public void SetTrumpUsable(bool trumpUsable)
        {
            this.trumpUsable = trumpUsable;
        }

        public RoundScore[] GetRoundScores()
        {
            if (this.roundScores != null)
            {
                return this.roundScores;
            }
            else
            {
                return activeBidding.GetRoundScores();
            }
            
        }
    }
}
