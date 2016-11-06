using Game.Actors;
using Game.Logic;
using System.Collections.Generic;
using System.Text;

namespace Game.Structure
{
    public class GameObject
    {
        protected GameTable gameTable;
        protected GameEndingCondition gameEndingCondition;
        protected int playedRoundCount;
        protected Round currentRound;

        public GameObject(GameTable gameTable,GameEndingCondition gameEndingCondition)
        {
            this.gameTable = gameTable;
            this.gameEndingCondition = gameEndingCondition;
            this.playedRoundCount = 0;
            ScoreBoard.ResetScores();
        }

        /// <summary>
        /// Depending on the game mode, checks if the condition is met. For example a score is reached.
        /// </summary>
        /// <returns>If the condition is met and the game should end or not</returns>
        public bool IsGameConditionMet()
        {
            return gameEndingCondition.isConditionMet();
        }

        public virtual void EndGame()
        {

            //TODO
        }

        public Round GetCurrentRound()
        {
            return currentRound;
        }

        public virtual void Commence()
        {
            currentRound = new Round(this, gameTable.GetPlayerSeatedAt(0));
            currentRound.InitiateBidding();
        }

        public virtual void Commence(int firstPlayer)
        {
            this.currentRound = new Round(this, gameTable.GetPlayerSeatedAt(firstPlayer));
            this.currentRound.InitiateBidding();
        }


        public virtual void RoundEnded(Player firstPlayer)
        {
            playedRoundCount++;
            if (IsGameConditionMet())
            {
                EndGame();
            }
            else
            {
                currentRound = new Round(this, gameTable.GetPlayerSeatedAt((gameTable.GetPlayersSeat(firstPlayer) + 1) % 4));
                currentRound.InitiateBidding();
            }
        }

        public int GetRoundCount()
        {
            return playedRoundCount;
        }

        public GameTable GetGameTableRefrence()
        {
            return gameTable;
        }
    }
}
