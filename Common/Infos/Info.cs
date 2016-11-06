using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Infos
{
    /// <summary>
    /// Class to pass towards the UI in order to inform the player
    /// </summary>
    public class Info
    {
        private int infoCode;


        public Info(InfoDescription description)
        {
            switch (description)
            {
                case InfoDescription.CannotPlayThatCard:
                    infoCode = 1;
                    break;
                case InfoDescription.BidNotValid:
                    infoCode = 2;
                    break;
            }
        }


        public int GetInfoCode()
        {
            return this.infoCode;
        }

    }
}
