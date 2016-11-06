using Game.Actors;
using System.Text;

namespace Game.Structure
{
    /// <summary>
    /// Represents one card played by a player
    /// </summary>
    public class Turn
    {
        private Player playedBy;
        private Common.Card playedCard;

        public Turn(Player playedBy,Common.Card playedCard)
        {
            this.playedBy = playedBy;
            this.playedCard = playedCard;
        }

        public Player GetPlayer()
        {
            return this.playedBy;
        }

        public Common.Card GetPlayedCard()
        {
            return this.playedCard;
        }
    }
}
