using Common.Enums;
using Game.Actors;
using System.Collections.Generic;
using System.Text;

namespace Game.Structure
{
    /// <summary>
    /// The Phase where we ask the players for the bidding 
    /// </summary>
    public class InitialPhase
    {
        protected RoundState biddings;
        private Round currentRound;
        private Player firstToAsk;

        public InitialPhase(Player firstToAsk,Round currentRound){
            this.currentRound = currentRound;
            biddings = new RoundState();
            this.firstToAsk = firstToAsk;
        }

        public void StartAsking(){
            firstToAsk.AskForAction(ActionType.DO_BIDDING, this);
        }

        public int GetLowestPossibleBid()
        {
            return biddings.GetLowestPossibleBid();
        }

        public void Answer(Player bidder,int bid)
        {
            if (biddings.SetBid(bidder.GetPlayersSeat(), bid))
            {
                //  A VALID BID
                if ((biddings.GetBidders().Count == 1 && bidder.GetPlayersSeat() == biddings.GetBidders()[0]) || bid == 13)
                {
                    // WINNER
                    biddings.WinnerCallback(bidder);
                    bidder.AskForAction(ActionType.SET_TRUMP_TYPE, this);
                }else if(biddings.GetBidders().Count == 1){
                    // WİNNER IS THE NEXT ONE
                    var winner = currentRound.GetGameObjectReference().GetGameTableRefrence().GetPlayerSeatedAt(biddings.GetBidders()[0]);
                    biddings.WinnerCallback(winner);
                    winner.AskForAction(ActionType.SET_TRUMP_TYPE, this);
                }else if(biddings.GetBidders().Count == 0 && biddings.HasAnyonePassed()){
                    //  NOBODY BIDS
                    var winner = firstToAsk.GetPreviousPlayer();
                    biddings.SetPlayerSaid(winner.GetPlayersSeat(), 5);
                    biddings.WinnerCallback(winner);
                    winner.AskForAction(ActionType.SET_TRUMP_TYPE, this);
                }
                else
                {
                    //  ASK TO THE NEXT BIDDER
                    var nextBidder = biddings.GetNextBidder(bidder);
                    if (nextBidder == -1)
                    {
                        bidder.AskForAction(ActionType.SET_TRUMP_TYPE, this);
                    }
                    else
                    {
                        currentRound.GetGameObjectReference().GetGameTableRefrence().GetPlayerSeatedAt(nextBidder).AskForAction(ActionType.DO_BIDDING, this);
                    }
                    
                }
            }
            else
            {
                bidder.AskForAction(ActionType.DO_BIDDING, this);
            }
        }

        public void setTrumpType(CardType trumpType,Player winner)
        {
            currentRound.BiddingCallback(trumpType, winner,biddings.GetRoundScores());
        }

        public RoundScore[] GetRoundScores(){
            return this.biddings.GetRoundScores();
        }
    }
}
