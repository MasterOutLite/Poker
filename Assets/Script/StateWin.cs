using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateWin
{
    public static void ShowWinCard (IGrouping<int, Card> cards)
    {
        var list = cards.ToList();
        ShowWinCard(list);
    }

    public static void ShowWinCard (List<Card> cards)
    {
        if (cards == null)
        {
            throw new ArgumentNullException("Cards is null");
        }

        foreach (var card in cards)
        {
            card.Image.color = Color.green;
        }
    }

    private static bool Contains (int val, List<Card> cardsCheck)
    {
        return cardsCheck.Any(c => c.CardInfo.Ordinal == val);
    }

    public static bool Pair (List<Card> cardsOnTable, List<Card> cardsInPlayer, bool showWin = true)
    {
        bool isContain = false;
        Card firstCard = cardsInPlayer[0];
        Card secondCard = cardsInPlayer[1];
        if (firstCard.CardInfo.Ordinal == secondCard.CardInfo.Ordinal)
        {
            Card contain =
                cardsOnTable.Find(card => card.CardInfo.Ordinal == firstCard.CardInfo.Ordinal);
            if (contain)
            {
                return false;
            }
            ShowWinCard(cardsInPlayer);
            return true;
        }


        foreach (var cardPlayer in cardsInPlayer)
        {
            Card contain =
                cardsOnTable.Find(card => card.CardInfo.Ordinal == cardPlayer.CardInfo.Ordinal);

            List<Card> contains =
             cardsOnTable.FindAll(card => card.CardInfo.Ordinal == cardPlayer.CardInfo.Ordinal);

            if (contains != null && contains.Count > 1)
            {               
                return false;
            }

            if (contain && showWin)
            {
                isContain = true;
                ShowWinCard(new List<Card> { contain, cardPlayer });
                break;
            }
        }

        return isContain;
    }

    public static bool TwoPair (List<Card> cardsOnTable, List<Card> cardsInPlayer)
    {
        bool isContain = false;
        Card firstCard = cardsInPlayer[0];
        Card secondCard = cardsInPlayer[1];

        if (firstCard.CardInfo.Ordinal == secondCard.CardInfo.Ordinal)
            return false;

        Card firsrContain =
            cardsOnTable.Find(card => card.CardInfo.Ordinal == firstCard.CardInfo.Ordinal);

        Card secondContain =
            cardsOnTable.Find(card => card.CardInfo.Ordinal == secondCard.CardInfo.Ordinal &&
                              card.CardInfo.Ordinal != firstCard.CardInfo.Ordinal);

        if (firsrContain != null && secondContain != null)
        {
            ShowWinCard(new List<Card> { firstCard, firsrContain, secondCard, secondContain });
            isContain = true;
        }

        return isContain;
    }

    public static bool ThreeOfAKind (List<Card> cardsInTable, List<Card> cardsInPlayer)
    {
        Card firstCard = cardsInPlayer[0];
        Card secondCard = cardsInPlayer[1];

        if (firstCard.CardInfo.Ordinal == secondCard.CardInfo.Ordinal)
        {
            Card containCard =
              cardsInTable.Find(card => card.CardInfo.Ordinal == firstCard.CardInfo.Ordinal);
            if (containCard)
            {
                ShowWinCard(new List<Card> { firstCard, secondCard, containCard });
                return containCard;
            }
        }

        List<Card> containsCardToFirst =
         cardsInTable.FindAll(card => card.CardInfo.Ordinal == firstCard.CardInfo.Ordinal);
        if (containsCardToFirst.Count == 2)
        {
            var winCard = new List<Card> { firstCard };
            winCard.AddRange(containsCardToFirst);
            ShowWinCard(winCard);
            return true;
        }

        List<Card> containsCardToSecond =
         cardsInTable.FindAll(card => card.CardInfo.Ordinal == secondCard.CardInfo.Ordinal);
        if (containsCardToFirst.Count == 2)
        {
            var winCard = new List<Card> { secondCard };
            winCard.AddRange(containsCardToFirst);
            ShowWinCard(winCard);
            return true;
        }

        return false;
    }

    public static bool FourOfAKind (List<Card> cardsCheck)
    {
        var cards = cardsCheck.GroupBy(h => h.CardInfo.Ordinal);

        bool isWin = cards.Any(g => g.Count() == 4);
        if (isWin)
            foreach (var item in cards)
            {
                if (item.ToArray().Length == 4)
                    ShowWinCard(item);
            }
        return isWin;
    }

    public static bool Flush (List<Card> cardsCheck)
    {
        var cards = cardsCheck.GroupBy(h => h.CardInfo.Ordinal);

        bool isWin = cards.Count() == 1;
        if (isWin)
            foreach (var item in cards)
            {
                ShowWinCard(item);
            }
        return isWin;
    }

    public static bool FullHouse (List<Card> cardsOnTable, List<Card> cardsInPlayer)
    {
        return Pair(cardsOnTable, cardsInPlayer, false) && ThreeOfAKind(cardsOnTable, cardsInPlayer);
    }

    public static bool Straight (List<Card> cardsCheck)
    {
        if (Contains((int)CartType.Туз, cardsCheck) &&
            Contains((int)CartType.Король, cardsCheck) &&
            Contains((int)CartType.Дама, cardsCheck) &&
            Contains((int)CartType.Валет, cardsCheck) &&
            Contains((int)CartType.Десять, cardsCheck))
        {
            ShowWinCard (cardsCheck);
            return true;
        }
        var ordered = cardsCheck.OrderBy(h => h.CardInfo.Ordinal).ToArray();
        var straightStart = (int)ordered.First().CardInfo.Ordinal;
        for (var i = 1; i < ordered.Length; i++)
        {
            if ((int)ordered[i].CardInfo.Ordinal != straightStart + i)
                return false;
        }
        ShowWinCard(ordered.ToList());
        return true;
    }

    public static bool StraightFlush (List<Card> cardsCheck)
    {
        return Straight(cardsCheck) && Flush(cardsCheck);
    }

    public static bool RoyalStraightFlush (List<Card> cardsCheck)
    {
        return Straight(cardsCheck) && Flush(cardsCheck) &&
            Contains((int)CartType.Туз, cardsCheck) && Contains((int)CartType.Король, cardsCheck);
    }

    public static string CheckWiner (List<Card> cardsOnTable, Player player, Rate rate)
    {
        string msg;
        float bet = 0;
        List<Card> playerCard = player.PlayerCard;
        List<Card> cards = new(cardsOnTable);
        cards.AddRange(playerCard);
        if (RoyalStraightFlush(cards))
        {
            bet = 10;
            msg = "У вас комбінація Стріт-Рояль-Флеш";
        }
        else if (StraightFlush(cards))
        {
            bet = 5;
            msg = "У вас комбінація Стріт-флеш";
        }
        else if (FullHouse(cardsOnTable, playerCard))
        {
            bet = 4.5f;
            msg = "У вас комбінація Фул хаус";
        }
        else if (Straight(cards))
        {
            bet = 4;
            msg = "У вас комбінація Стріт";
        }
        else if (Flush(cards))
        {
            bet = 3.5f;
            msg = "У вас комбінація Флеш";
        }
        else if (FourOfAKind(cards))
        {
            bet = 3;
            msg = "У вас комбінація Четвьорка";
        }
        else if (ThreeOfAKind(cardsOnTable, playerCard))
        {
            bet = 2.5f;
            msg = "У вас комбінація Тройка";
        }
        else if (TwoPair(cardsOnTable, playerCard))
        {
            bet = 2;
            msg = "У вас комбінація Дві пари";
        }
        else if (Pair(cardsOnTable, playerCard))
        {
            bet = 1.5f;
            msg = "У вас комбінація Пара";
        }
        else
        {
            player.Money -= rate.Scope;
            msg = "Вам нічого не випало";
        }

        player.Money += rate.Scope * bet;
        
        return msg;
    }
}
