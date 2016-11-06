using Common.Enums;
using Common.Helpers;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Card
    {
        #region PRIVATE MEMBERS

        private CardValue cardValue;
        private int numericValue;
        private CardType cardType;

        #endregion

        #region CONSTRUCTORS

        public Card(int value, CardType type)
        {
            this.cardType = type;
            this.SetNumericCardValue(value);
        }

        public Card(CardValue value, CardType type)
        {
            this.cardType = type;
            this.SetCardValue(value);
        }

        public Card(CardValue value, int type)
        {
            this.cardType = CardHelper.GetCardTypeFromValue(type);
            this.SetCardValue(value);
        }

        public Card(int value, int type)
        {
            this.cardType = CardHelper.GetCardTypeFromValue(type);
            this.SetNumericCardValue(value);
        }

        #endregion

        #region GETTER/SETTERS
        public CardValue GetCardValue()
        {
            return this.cardValue;
        }

        public void SetCardValue(CardValue newValue)
        {
            this.cardValue = newValue;
            this.numericValue = CardHelper.GetValueFromCardEnum(newValue);
        }

        public void SetNumericCardValue(int newValue)
        {
            this.numericValue = newValue;
            this.cardValue = CardHelper.GetCardEnumFromValue(newValue);
        }

        public int GetNumericValue()
        {
            return this.numericValue;
        }

        public CardType GetCardType()
        {
            return cardType;
        }

        #endregion


        #region PUBLIC METHODS


        public string ToCardString()
        {
            return GetCardType().ToString("G") + GetCardValue().ToString("G");
        }

        public int CalculateRealValue(int trumpType)
        {
            if (trumpType == CardHelper.GetNumericTypeFromEnum(this.cardType))
            {
                return this.numericValue + 14;
            }
            else
            {
                return this.numericValue;
            }
        }

        public int CalculateRealValue(CardType trumpType)
        {
            if (trumpType == this.cardType)
            {
                return this.numericValue + 14;
            }
            else
            {
                return this.numericValue;
            }
        }

        public string ToNetworkCardString()
        {
            return Common.Helpers.CardHelper.GetNumericTypeFromEnum(this.GetCardType()).ToString() + "-" + this.GetNumericValue().ToString();
        }

        #endregion


    }
}
