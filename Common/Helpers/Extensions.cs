using Common;
using Common.Enums;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public static class Extensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (System.Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static bool Contains(this IList<Common.Card> list, CardType cardType, CardValue cardValue)
    {
        foreach (Card card in list)
        {
            if (card.GetCardValue() == cardValue && card.GetCardType() == cardType)
            {
                return true;
            }
        }

        return false;
    }

    public static Card GetCard(this IList<Common.Card> list, CardType cardType, CardValue cardValue)
    {
        foreach (Card card in list)
        {
            if (card.GetCardValue() == cardValue && card.GetCardType() == cardType)
            {
                return card;
            }
        }

        return null;
    }


    public static Card GetSomeCardBiggerThan(this IList<Common.Card> list, Card card)
    {
        int i = 0;
        while (i < list.Count)
        {
            if (list[i].GetCardType() == card.GetCardType() && list[i].GetNumericValue() >= card.GetNumericValue())
            {
                return list[i];
            }
            i++;
        }
        return null;
    }

    public static Card GetCardOfType(this IList<Common.Card> list, CardType type)
    {
        int i = 0;
        while (i < list.Count)
        {
            if (list[i].GetCardType() == type)
            {
                return list[i];
            }
            i++;
        }
        return null;
    }
}