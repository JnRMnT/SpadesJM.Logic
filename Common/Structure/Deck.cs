using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Runtime.CompilerServices;
using Common.Enums;
using Common.Helpers;

namespace Common.Structure
{
    public class Deck
    {
        private List<Card> cards;

        private float deckValue;
        private CardType trumpToChoose;

        public Deck()
        {
            cards = new List<Card>();
        }

        public Deck(List<Card> cards)
        {
            this.cards = cards;
            cards.Sort(new CardComparer());
        }

        public float GetDeckValue()
        {
            return deckValue;
        }
        public void SetDeckValue(float deckValue)
        {
            this.deckValue = deckValue;
        }

        public CardType GetTrumpToChoose()
        {
            return this.trumpToChoose;
        }
        public void SetTrumpToChoose(CardType trumpToChoose)
        {
            this.trumpToChoose = trumpToChoose;
        }


        public List<Card>[] Deal()
        {
            List<Card>[] splittedDeck = new List<Card>[4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    cards.Add(new Card(j + 2, i));
                }
            }

            cards.Shuffle();

            splittedDeck[0] = cards.GetRange(0, 13);
            splittedDeck[1] = cards.GetRange(13, 13);
            splittedDeck[2] = cards.GetRange(26, 13);
            splittedDeck[3] = cards.GetRange(39, 13);

            return splittedDeck;
        }

        public List<Card> GetCards()
        {
            return this.cards;
        }

        public bool ContainsCardOfType(CardType cardType)
        {
            foreach (Card card in this.cards)
            {
                if (card.GetCardType() == cardType)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsCard(CardType type, CardValue value)
        {
            foreach (Card card in cards)
            {
                if (card.GetCardType() == type && card.GetCardValue() == value)
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsTrumpCard(CardType trumpType)
        {
            return ContainsCardOfType(trumpType);
        }

        public bool ContaintsAnythingBetter(int realValue, CardType handCardType, CardType trumpType, bool trumpUsable)
        {
            foreach (Card cardInDeck in this.cards)
            {
                if ((cardInDeck.GetCardType() == handCardType || trumpUsable) && cardInDeck.CalculateRealValue(trumpType) > realValue)
                {
                    return true;
                }
            }

            return false;
        }

        public void UseCard(Card cardToPlay)
        {
            this.cards.Remove(cardToPlay);
        }

        public bool ContainsOnlyTypeOf(CardType type)
        {
            //  DIAMOND
            if (ContainsCardOfType(CardType.Diamond) && type != CardType.Diamond)
            {
                return false;
            }
            else if (ContainsCardOfType(CardType.Spade) && type != CardType.Spade)
            {
                //  SPADE
                return false;
            }
            else if (ContainsCardOfType(CardType.Club) && type != CardType.Club)
            {
                //  CLUB
                return false;
            }
            else if (ContainsCardOfType(CardType.Heart) && type != CardType.Heart)
            {
                //  HEART
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
