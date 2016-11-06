using Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Helpers
{
    public class CardComparer : IComparer<Card>
    {
        private bool useTypeOffset;
        public CardComparer(bool useTypeOffset)
        {
            this.useTypeOffset = useTypeOffset;
        }

        public CardComparer()
        {
            this.useTypeOffset = true;
        }

        public int Compare(Card first, Card second)
        {
            if (first != null && second != null)
            {
                // We can compare both properties.
                var typeOffsetOne = calculateTypeOffset(first.GetCardType());
                var typeOffsetTwo = calculateTypeOffset(second.GetCardType());

                var firstValue = typeOffsetOne + first.GetCardValue();
                var secondValue = typeOffsetTwo + second.GetCardValue();

                return secondValue.CompareTo(firstValue);
            }

            if (first == null && second == null)
            {
                // We can't compare any properties, so they are essentially equal.
                return 0;
            }

            if (first != null)
            {
                // Only the first instance is not null, so prefer that.
                return -1;
            }

            // Only the second instance is not null, so prefer that.
            return 1;
        }

        private int calculateTypeOffset(CardType type)
        {
            var retValue = 0;

            if (!this.useTypeOffset)
            {
                return 0;
            }

            switch (type)
            {
                case CardType.Club:
                    retValue = 0;
                    break;
                case CardType.Diamond:
                    retValue = 15;
                    break;
                case CardType.Spade:
                    retValue = 30;
                    break;
                case CardType.Heart:
                    retValue = 45;
                    break;
            }
            return retValue;
        }
    }
}
