using Common;
using Common.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Game.Logic
{
    public class PlayedCards
    {
        protected static List<Card> playedCards = new List<Card>();

        public static List<Card> GetPlayedCards()
        {
            return playedCards;
        }

        public static void AddPlayedCard(Card playedCard)
        {
            playedCards.Add(playedCard);
        }

        public static void ResetPlayedCards()
        {
            playedCards.Clear();
        }

        public static bool HasBeenPlayed(CardType cardType, CardValue cardValue)
        {
            return playedCards.Contains(cardType, cardValue);
        }

        public static int PlayedCardCountOfType(CardType type)
        {
            int count = 0;

            foreach (Card card in playedCards)
            {
                if (card.GetCardType() == type)
                {
                    count++;
                }
            }

            return count;
        }

        public static bool NoBodyUsesTrumpTo(CardType type, CardType trumpType)
        {
            bool trumpUsed = false;
            for (int i = 0; i < playedCards.Count / 4; i++)
            {
                if (playedCards[i * 4].GetCardType() == type)
                {
                    if (playedCards[i * 4 + 1].GetCardType() == trumpType || playedCards[i * 4 + 2].GetCardType() == trumpType || playedCards[i * 4 + 3].GetCardType() == trumpType)
                    {
                        trumpUsed = true;
                    }
                }
            }
            return trumpUsed;
        }
    }
}
