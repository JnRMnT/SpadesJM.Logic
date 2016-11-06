using Game.Actors;
using System.Collections.Generic;
using System.Text;

namespace Game.Structure
{
    public class RoundState
    {
        private RoundScore[] roundScores;

        public RoundState()
        {
            roundScores = new RoundScore[4];

            for (int i = 0; i < 4; i++)
            {
                roundScores[i] = new RoundScore();
                roundScores[i].SetSaid(-2);
                roundScores[i].SetGot(0);
            }
        }

        public RoundScore[] GetRoundScores()
        {
            return this.roundScores;
        }

        public int GetNextBidder(Player currentBidder)
        {
            List<int> bidders = GetBidders();

            if(bidders.Count == 0){
                return -1;
            }else{
                var nextBidder = (currentBidder.GetPlayersSeat()+1) % 4;
                while(!bidders.Contains(nextBidder)){
                    nextBidder = (nextBidder + 1) % 4;
                }
                return nextBidder;
            }
           
        }

        public int GetLowestPossibleBid()
        {
            var highestBid = -5;
            for (int i = 0; i < 4; i++)
            {
                if (roundScores[i].GetSaid()!= -2 && roundScores[i].GetSaid() != -1 && roundScores[i].GetSaid() != 0 && roundScores[i].GetSaid() > highestBid)
                {
                    highestBid = roundScores[i].GetSaid();
                }
            }
            if (highestBid == -5)
            {
                return 5;
            }
            else
            {
                return highestBid + 1;
            }
        }

        public List<int> GetBidders()
        {
            List<int> bidders = new List<int>(4);
            for (int i = 0; i < 4; i++)
            {
                if (roundScores[i].GetSaid()==-2 || (roundScores[i].GetSaid() != -1 && roundScores[i].GetSaid() != 0))
                {
                    bidders.Add(i);
                }
            }
            return bidders;
        }

        public bool SetBid(int playerSeat,int bid){
            if (isValid(playerSeat, bid))
            {
                if (this.roundScores[playerSeat] == null)
                {
                    this.roundScores[playerSeat] = new RoundScore();
                }
                this.roundScores[playerSeat].SetSaid(bid);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isValid(int playerSeat, int bid)
        {
            if(bid==0 || bid == -1){
                return true;
            }else if(bid<4){
                return false;
            }
            else if (bid > 13)
            {
                return false;
            }

            bool valid = true;
            for (int i = 0; i < 4; i++)
            {
                if (roundScores[i]!=null && roundScores[i].GetSaid() >= bid)
                {
                    valid = false;
                }
            }

            return valid;
        }


        public void WinnerCallback(Player bidder)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i != bidder.GetPlayersSeat() && roundScores[i].GetSaid()!=0)
                {
                    roundScores[i].SetSaid(-1);
                }
            }
        }

        public bool HasAnyonePassed()
        {
            bool retValue = true;

            for (int i = 0; i < 4; i++)
            {
                if (roundScores[i].GetSaid() != 0 && roundScores[i].GetSaid() != -1)
                {
                    retValue = false;
                }
            }

            return retValue;
        }

        public void SetPlayerSaid(int playerSeat, int said)
        {
            this.roundScores[playerSeat].SetSaid(said);
        }
    }
}
