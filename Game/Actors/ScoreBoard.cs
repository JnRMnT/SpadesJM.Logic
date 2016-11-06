using System.Collections.Generic;
using System.Text;

namespace Game.Actors
{
    public static class ScoreBoard
    {
        public static GameTable gameTable;
        private static int[] scores= new int[4];

        public static void ResetScores()
        {
            scores = new int[4];
        }

        public static int GetPlayerScore(Player player)
        {
            return scores[gameTable.GetPlayersSeat(player)];
        }
        public static int GetPlayerScore(int playerSeat)
        {
            return scores[playerSeat];
        }

        public static Player GetLeadingPlayer()
        {
            var lead = GetLeadingPlayerSeat();
            return gameTable.GetPlayerSeatedAt(lead);
        }

        public static void IncrementScore(Player player)
        {
            scores[player.GetPlayersSeat()]++;
        }

        public static int GetLeadingPlayerSeat()
        {
            int lead = 0;
            for (int i = 0; i < 4; i++)
            {
                if (scores[i] > scores[lead])
                {
                    lead = i;
                }
            }

            return lead;
        }

        public static int GetLeadingScore()
        {
            var lead = GetLeadingPlayerSeat();
            return scores[lead];
        }


        public static void UpdateScores(Structure.RoundScore[] roundScores,int initiater)
        {

            for (int i = 0; i < 4; i++)
            {
                var said = roundScores[i].GetSaid();
                var got = roundScores[i].GetGot();
                if (said == 0)
                {
                    if (got == 0)
                    {
                        scores[i] += 50;
                    }
                    else
                    {
                        scores[i] -= 50;
                    }
                }
                else if(said==-1)
                {
                    if (got < 2)
                    {
                        //  GOING DOWN
                        scores[i] -= roundScores[initiater].GetSaid() * 10;
                    }
                    else
                    {
                        scores[i] += got * 10;
                    }
                }
                else if (said > got)
                {
                    scores[i] -= said * 10;
                }
                else
                {
                    scores[i] += (got - said);
                    scores[i] += said * 10;
                }
            }
        }
    }
}
