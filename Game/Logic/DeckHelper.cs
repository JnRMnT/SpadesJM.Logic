using Common;
using Common.Enums;
using Common.Structure;
using Game.Actors;
using Game.Logic.Comparers;
using Game.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using Common.Helpers;

namespace Game.Logic
{
    public static class DeckHelper
    {

        public static DeckValue[] CalculateDeckValues(Deck deck)
        {
            List<DeckValue> deckValues = new List<DeckValue>(4);

            //  CLUB
            deckValues.Add(new DeckValue());
            deckValues[0].trumpType = CardType.Club;
            deckValues[0].value = CalculateValueWithTrumpType(CardType.Club, deck);

            // DIAMOND
            deckValues.Add(new DeckValue());
            deckValues[1].trumpType = CardType.Diamond;
            deckValues[1].value = CalculateValueWithTrumpType(CardType.Diamond, deck);

            // SPADE
            deckValues.Add(new DeckValue());
            deckValues[2].trumpType = CardType.Spade;
            deckValues[2].value = CalculateValueWithTrumpType(CardType.Spade, deck);

            //  HEART
            deckValues.Add(new DeckValue());
            deckValues[3].trumpType = CardType.Heart;
            deckValues[3].value = CalculateValueWithTrumpType(CardType.Heart, deck);

            deckValues.Sort(new TrumpSorter());
            return deckValues.ToArray();

        }

        private static float CalculateValueWithTrumpType(CardType trump, Deck deck)
        {
            float value = 0;

            //  CLUB
            List<Card> clubCards = GetCardsOfType(CardType.Club, deck);
            if (trump == CardType.Club)
            {
                value += CalculateDeckTrumpValue(clubCards);
            }
            else
            {
                value += CalculateCardTypeValue(CardType.Club, clubCards, deck);
            }

            //  DIAMOND
            List<Card> diamondCards = GetCardsOfType(CardType.Diamond, deck);
            if (trump == CardType.Diamond)
            {
                value += CalculateDeckTrumpValue(diamondCards);
            }
            else
            {
                value += CalculateCardTypeValue(CardType.Diamond, diamondCards, deck);
            }

            //  HEART
            List<Card> heartCards = GetCardsOfType(CardType.Heart, deck);
            if (trump == CardType.Heart)
            {
                value += CalculateDeckTrumpValue(heartCards);
            }
            else
            {
                value += CalculateCardTypeValue(CardType.Heart, heartCards, deck);
            }

            //  SPADE
            List<Card> spadeCards = GetCardsOfType(CardType.Spade, deck);
            if (trump == CardType.Spade)
            {
                value += CalculateDeckTrumpValue(spadeCards);
            }
            else
            {
                value += CalculateCardTypeValue(CardType.Spade, spadeCards, deck);
            }

            return value;
        }

        private static float CalculateCardTypeValue(CardType cardType, List<Card> cardsOfType, Deck deck)
        {
            float value = 0;
            int goodCards = 0;

            if (deck.ContainsCard(cardType, CardValue.ACE))
            {
                //  HAS ACE
                value += 1;
                goodCards++;
                if (deck.ContainsCard(cardType, CardValue.KING))
                {
                    //  HAS ACE + KING
                    value += 1;
                    goodCards++;
                    if (deck.ContainsCard(cardType, CardValue.QUEEN))
                    {
                        // HAS ACE + KING + QUEEN
                        value += 1;
                        goodCards++;
                    }
                }
                else
                {
                    if (deck.ContainsCard(cardType, CardValue.QUEEN))
                    {
                        //  HAS ACE + QUEEN
                        goodCards++;
                        if (cardsOfType.Count > 2)
                        {
                            //  HAS MORE THAN 2 CARDS OF THAT TYPE
                            value += 1;
                        }
                        else
                        {
                            value += 0.4f;
                        }
                    }

                }
            }
            else if (deck.ContainsCard(cardType, CardValue.KING))
            {
                //  HAS KING
                goodCards++;
                if (cardsOfType.Count > 1)
                {
                    //  HAS MORE THAN 1 CARDS OF THAT TYPE
                    value += 1f;
                }
                else
                {
                    value += 0.5f;
                }

                if (deck.ContainsCard(cardType, CardValue.QUEEN))
                {
                    //  HAS KING + QUEEN
                    goodCards++;
                    if (cardsOfType.Count > 2)
                    {
                        value += 0.8f;
                    }
                    else
                    {
                        value += 0.3f;
                    }
                }
            }
            else if (deck.ContainsCard(cardType, CardValue.QUEEN))
            {
                //  HAS QUEEN
                goodCards++;
                if (cardsOfType.Count > 2)
                {
                    value += 0.5f;
                }
            }

            value += (cardsOfType.Count - goodCards) / 5;

            return value;
        }

        private static float CalculateDeckTrumpValue(List<Card> clubCards)
        {
            float value = 0;

            foreach (Card card in clubCards)
            {
                if (card.GetNumericValue() > 10)
                {
                    value += 1f;
                }
                else
                {
                    value += card.GetNumericValue() / 10;
                }
            }

            return value;
        }

        public static List<Card> GetCardsOfType(CardType type, Deck deck)
        {
            List<Card> returnList = new List<Card>();

            foreach (Card card in deck.GetCards())
            {
                if (card.GetCardType() == type)
                {
                    returnList.Add(card);
                }
            }

            return returnList;
        }

        public static Card GetBestMove(Hand currentHand, Deck deck, Player player)
        {
            CardType trumpType = currentHand.GetCurrentRoundReference().GetTrumpType();
            bool firstToPlay = currentHand.GetInitiater().GetPlayersSeat() == player.GetPlayersSeat();
            bool trumpUsable = currentHand.GetCurrentRoundReference().isTrumpUsable();
            List<Card> availableMoves = GetAvailableMoves(currentHand, deck, player);
            List<Card> bestMoves = new List<Card>();

            if (firstToPlay)
            {
                //  PLAYER INITIATES THE HAND
                bool playTrump = false;
                Card bestTrumpMove = null;
                if (trumpUsable && (13 - PlayedCards.PlayedCardCountOfType(trumpType)) / 4 < GetCardsOfType(trumpType, deck).Count)
                {
                    //  COLLECT TRUMPS
                    bestTrumpMove = GetBestMoveOfType(trumpType, availableMoves, trumpType, deck);
                    if (bestTrumpMove != null && bestTrumpMove.ToCardString() != String.Empty)
                    {
                        playTrump = true;
                    }
                }
                if (playTrump)
                {
                    return bestTrumpMove;
                }
                else
                {

                    Card bestMoveDiamond = GetBestMoveOfType(CardType.Diamond, availableMoves, trumpType, deck);
                    if (bestMoveDiamond != null && PlayedCards.NoBodyUsesTrumpTo(CardType.Diamond, trumpType))
                    {
                        bestMoves.Add(bestMoveDiamond);
                    }
                    Card bestMoveSpade = GetBestMoveOfType(CardType.Spade, availableMoves, trumpType, deck);
                    if (bestMoveSpade != null && PlayedCards.NoBodyUsesTrumpTo(CardType.Spade, trumpType))
                    {
                        bestMoves.Add(bestMoveSpade);
                    }
                    Card bestMoveClub = GetBestMoveOfType(CardType.Club, availableMoves, trumpType, deck);
                    if (bestMoveClub != null && PlayedCards.NoBodyUsesTrumpTo(CardType.Club, trumpType))
                    {
                        bestMoves.Add(bestMoveClub);
                    }
                    Card bestMoveHeart = GetBestMoveOfType(CardType.Heart, availableMoves, trumpType, deck);
                    if (bestMoveHeart != null && PlayedCards.NoBodyUsesTrumpTo(CardType.Heart, trumpType))
                    {
                        bestMoves.Add(bestMoveHeart);
                    }

                    if (bestMoves.Count != 0)
                    {
                        bestMoves.Sort(new CardComparer(false));
                        return bestMoves[bestMoves.Count - 1];
                    }
                    else
                    {
                        return availableMoves[0];
                    }
                }
            }
            else
            {
                //  THIS IS AN ONGOING HAND
                CardType handType = currentHand.GetPlayedCards()[0].GetPlayedCard().GetCardType();
                //  GOING TO USE TRUMP
                if (!deck.ContainsCardOfType(handType))
                {
                    if ((PlayedCards.NoBodyUsesTrumpTo(handType, trumpType) && PlayedCards.PlayedCardCountOfType(handType) < 2) || currentHand.GetPlayedCardCount() == 3)
                    {
                        return availableMoves[0];
                    }
                    else if (PlayedCards.PlayedCardCountOfType(handType) > 2)
                    {
                        //  SOMEBODY USES TRUMP
                        Card cardToPlay = availableMoves[0];
                        for (int i = availableMoves.Count - 1; i >= 0; i--)
                        {
                            if (availableMoves[i].GetCardValue() == CardValue.ACE)
                            {
                                //  IF HAS ACE AND DOESNT HAVE KING PLAY IT
                                if (!availableMoves.Contains(new Card(CardValue.KING, trumpType)))
                                {
                                    cardToPlay = availableMoves[i];
                                }
                            }
                            else if (availableMoves[i].GetCardValue() == CardValue.KING && (PlayedCards.HasBeenPlayed(trumpType, CardValue.ACE) || availableMoves.Contains(new Card(CardValue.ACE, trumpType))))
                            {
                                //  HAS ACE OR ACE HAS ALREADY BEEN PLAYED / PLAY THE KING
                                cardToPlay = availableMoves[i];
                            }
                            else if (availableMoves[i].GetCardValue() == CardValue.JOKER && (PlayedCards.HasBeenPlayed(trumpType, CardValue.KING) || availableMoves.Contains(new Card(CardValue.KING, trumpType))
                                && ((PlayedCards.HasBeenPlayed(trumpType, CardValue.ACE) || availableMoves.Contains(new Card(CardValue.ACE, trumpType))))))
                            {
                                //  PLAY THE QUEEN
                                cardToPlay = availableMoves[i];
                            }
                        }
                        return cardToPlay;
                    }
                    else
                    {
                        return availableMoves[0];
                    }
                }
                else
                {
                    //  PLAY THE SAME TYPE OF CARD AS THE HAND
                    if (PlayedCards.NoBodyUsesTrumpTo(handType, trumpType) && currentHand.GetPlayedCardCount() != 3)
                    {
                        //  NOBODY USES TRUMP
                        if (ContainsCards(new Card[] { new Card(CardValue.ACE, handType), new Card(CardValue.KING, handType), new Card(CardValue.QUEEN, handType) }, availableMoves))
                        {
                            //  HAS ACE, KING, QUEEN = PLAY QUEEN
                            return availableMoves.GetCard(handType, CardValue.QUEEN);
                        }
                        else if (ContainsCards(new Card[] { new Card(CardValue.ACE, handType), new Card(CardValue.KING, handType) }, availableMoves))
                        {
                            //  HAS ACE, KING = PLAY KING
                            return availableMoves.GetCard(handType, CardValue.KING);
                        }
                        else if (ContainsCards(new Card[] { new Card(CardValue.QUEEN, handType) }, availableMoves) && PlayedCards.HasBeenPlayed(handType, CardValue.ACE) && PlayedCards.HasBeenPlayed(handType, CardValue.KING))
                        {
                            //  HAS QUEEN, ACE AND KING HAS BEEN PLAYED
                            return availableMoves.GetCard(handType, CardValue.KING);
                        }
                        else
                        {
                            return availableMoves[0];
                        }
                    }
                    else
                    {
                        //  SOMEBODY USES TRUMP OR WE ARE THE LAST ONE TO PLAY
                        return availableMoves[0];
                    }
                }

            }
        }


        private static Card GetBestMoveOfType(CardType type, List<Card> availableMoves, CardType trumpType, Deck deck)
        {
            if (ContainsCards(new Card[] { new Card(CardValue.ACE, type), new Card(CardValue.KING, type), new Card(CardValue.QUEEN, type) }, availableMoves))
            {
                //  HAS ACE, KING, QUEEN = PLAY QUEEN
                return availableMoves.GetCard(type, CardValue.QUEEN);
            }
            else if (ContainsCards(new Card[] { new Card(CardValue.ACE, type), new Card(CardValue.KING, type) }, availableMoves))
            {
                //  HAS ACE, KING = PLAY KING
                return availableMoves.GetCard(type, CardValue.KING);
            }
            else if (ContainsCards(new Card[] { new Card(CardValue.QUEEN, type) }, availableMoves) && PlayedCards.HasBeenPlayed(type, CardValue.ACE) && PlayedCards.HasBeenPlayed(type, CardValue.KING)
                && PlayedCards.NoBodyUsesTrumpTo(type, trumpType))
            {
                //  HAS QUEEN, ACE AND KING HAS BEEN PLAYED AND NOBODY USES TRUMP
                return availableMoves.GetCard(type, CardValue.KING);
            }
            else if (availableMoves.Contains(type, CardValue.KING) && PlayedCards.HasBeenPlayed(type, CardValue.ACE))
            {
                //  HAS KING, ACE HAS BEEN PLAYED
                return availableMoves.GetCard(type, CardValue.KING);
            }
            else if (availableMoves.Contains(type, CardValue.ACE) && PlayedCards.PlayedCardCountOfType(type) > 2 && PlayedCards.NoBodyUsesTrumpTo(type, trumpType))
            {
                //  THIS TYPE HAS BEEN USED TWICE, USE THE ACE
                return availableMoves.GetCard(type, CardValue.ACE);
            }
            else if (availableMoves.Contains(type, CardValue.KING) && !availableMoves.Contains(type, CardValue.ACE) && GetCardsOfType(type, deck).Count > 2)
            {
                //  HAS KING BUT NOT ACE TRY TO GET THEM TO PLAY THE ACE
                Card cardToPlay = availableMoves.GetSomeCardBiggerThan(new Card(CardValue.SEVEN, type));
                if (cardToPlay == null || cardToPlay.GetNumericValue() > 12)
                {
                    cardToPlay = availableMoves.GetCardOfType(type);
                }
                return cardToPlay;
            }
            else
            {
                return null;
            }
        }

        private static bool ContainsCards(Card[] cards, List<Card> availableMoves)
        {
            int containingCards = 0;
            foreach (Card card in cards)
            {
                if (availableMoves.Contains(card.GetCardType(), card.GetCardValue()))
                {
                    containingCards++;
                }
            }

            return containingCards == cards.Length;
        }

        public static List<Card> GetAvailableMoves(Hand currentHand, Deck deck, Player player)
        {
            List<Card> validMoves = new List<Card>();
            Turn[] playedCars = currentHand.GetPlayedCards();
            Round currentRound = currentHand.GetCurrentRoundReference();
            CardType trumpType = currentRound.GetTrumpType();

            if (currentHand.GetInitiater() == player)
            {
                //  THIS PLAYER STARTS THE HAND
                var trumpUsable = currentRound.isTrumpUsable();
                foreach (Card card in deck.GetCards())
                {
                    if (card.GetCardType() != trumpType || trumpUsable || deck.ContainsOnlyTypeOf(trumpType))
                    {
                        validMoves.Add(card);
                    }
                }
            }
            else
            {
                var mostValuableCardInRound = currentHand.GetMostValuableCard().GetPlayedCard().CalculateRealValue(trumpType);
                var handCardType = playedCars[0].GetPlayedCard().GetCardType();
                foreach (Card card in deck.GetCards())
                {
                    if (
                        //  SAME TYPE OF CARD AND ITS VALUE IS GREATER
                        (handCardType == card.GetCardType() && card.CalculateRealValue(trumpType) > mostValuableCardInRound) ||
                        //  DOESNT HAS THAT TYPE AND THIS IS A TRUMP CARD
                        (card.GetCardType() == trumpType && !deck.ContainsCardOfType(handCardType) && card.CalculateRealValue(trumpType) > mostValuableCardInRound)
                        )
                    {
                        validMoves.Add(card);
                    }
                    else if (!deck.ContaintsAnythingBetter(mostValuableCardInRound, handCardType, trumpType, !deck.ContainsCardOfType(handCardType) && deck.ContainsCardOfType(trumpType)))
                    {
                        if (handCardType == card.GetCardType())
                        {
                            // HAS TO PLAY SAME TYPE
                            validMoves.Add(card);
                        }
                        else if (!deck.ContainsCardOfType(handCardType) && handCardType != card.GetCardType() && card.GetCardType() == trumpType)
                        {
                            // HAS TO PLAY TRUMP TYPE
                            validMoves.Add(card);
                        }
                        else if (!deck.ContainsCardOfType(handCardType) && !deck.ContainsCardOfType(trumpType))
                        //  DOESNT HAVE ANYTHING BETTER THAN MOST VALUEABLE CARD
                        {
                            validMoves.Add(card);
                        }
                    }
                }
            }

            validMoves.Sort(new CardComparer(false));
            return validMoves;
        }

        public static bool IsValidMove(Hand currentHand, Deck deck, Card card, Player player)
        {
            return GetAvailableMoves(currentHand, deck, player).Contains(card.GetCardType(), card.GetCardValue());
        }


    }
}
