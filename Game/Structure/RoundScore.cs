using System.Collections.Generic;
using System.Text;

namespace Game.Structure
{
    public class RoundScore
    {
        private int said, got;

        public RoundScore()
        {
            said = 0;
            got = 0;
        }

        public int GetSaid()
        {
            return said;
        }

        public void SetSaid(int said)
        {
            this.said = said;
        }

        public int GetGot()
        {
            return got;
        }

        public void SetGot(int got)
        {
            this.got = got;
        }

        public void IncrementGot()
        {
            this.got++;
        }
    }
}
