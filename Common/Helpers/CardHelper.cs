using Common.Enums;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public static class CardHelper
    {
        public static CardValue GetCardEnumFromValue(int value)
        {
            CardValue retValue = CardValue.ACE;

            switch (value)
            {
                case 2:
                    retValue = CardValue.TWO;
                    break;
                case 3:
                    retValue = CardValue.THREE;
                    break;
                case 4:
                    retValue = CardValue.FOUR;
                    break;
                case 5:
                    retValue = CardValue.FIVE;
                    break;
                case 6:
                    retValue = CardValue.SIX;
                    break;
                case 7:
                    retValue = CardValue.SEVEN;
                    break;
                case 8:
                    retValue = CardValue.EIGHT;
                    break;
                case 9:
                    retValue = CardValue.NINE;
                    break;
                case 10:
                    retValue = CardValue.TEN;
                    break;
                case 11:
                    retValue = CardValue.JOKER;
                    break;
                case 12:
                    retValue = CardValue.QUEEN;
                    break;
                case 13:
                    retValue = CardValue.KING;
                    break;
                case 14:
                    retValue = CardValue.ACE;
                    break;
            }
            return retValue;
        }

        public static int GetValueFromCardEnum(CardValue value)
        {
            int retValue = 14;
            switch (value)
            {
                case CardValue.TWO:
                    retValue = 2;
                    break;
                case CardValue.THREE:
                    retValue = 3;
                    break;
                case CardValue.FOUR:
                    retValue = 4;
                    break;
                case CardValue.FIVE:
                    retValue = 5;
                    break;
                case CardValue.SIX:
                    retValue = 6;
                    break;
                case CardValue.SEVEN:
                    retValue = 7;
                    break;
                case CardValue.EIGHT:
                    retValue = 8;
                    break;
                case CardValue.NINE:
                    retValue = 9;
                    break;
                case CardValue.TEN:
                    retValue = 10;
                    break;
                case CardValue.JOKER:
                    retValue = 11;
                    break;
                case CardValue.QUEEN:
                    retValue = 12;
                    break;
                case CardValue.KING:
                    retValue = 13;
                    break;
                case CardValue.ACE:
                    retValue = 14;
                    break;
            }
            return retValue;
        }

        public static int GetNumericTypeFromEnum(CardType type)
        {
            int retValue = 0;
            switch (type)
            {
                case CardType.Spade:
                    retValue = 0;
                    break;
                case CardType.Heart:
                    retValue = 1;
                    break;
                case CardType.Diamond:
                    retValue = 2;
                    break;
                case CardType.Club:
                    retValue = 3;
                    break;

            }
            return retValue;
        }

        public static CardType GetCardTypeFromValue(int value)
        {
            CardType retValue = CardType.Spade;
            switch (value)
            {
                case 0:
                    retValue = CardType.Spade;
                    break;
                case 1:
                    retValue = CardType.Heart;
                    break;
                case 2:
                    retValue = CardType.Diamond;
                    break;
                case 3:
                    retValue = CardType.Club;
                    break;
            }
            return retValue;

        }

        public static Card StringToCard(string networkCardString)
        {
            string[] cardProperties = networkCardString.Split('-');
            Card newCard = new Card(int.Parse(cardProperties[1]), int.Parse(cardProperties[0]));
            return newCard;
        }

    }
}
