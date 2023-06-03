using System.Collections.Generic;
using UnityEngine;

public class CardStore : MonoBehaviour
{
    [SerializeField] Player Player;
    [SerializeField] Table Table;
    [SerializeField] Rate Rate;
    [SerializeField] CardInfoStore _cards;
    [SerializeField] List<int> _usedIndexCard;
    [SerializeField] ShowGameInfo _showGameInfo;

    [SerializeField] private bool isFinish = false;

    public List<CardInfo> Cards => _cards.Cards;


    void Start ()
    {
        if (Player == null)
            Player = FindObjectOfType<Player>();

        if (Table == null)
            Table = FindObjectOfType<Table>();

        if (Rate == null)
            Rate = FindObjectOfType<Rate>();

        Debug.Log(Player.PlayerCard.Count);

        //_usedIndexCard = new List<int>();

        //int randIndex = GenerateIndexCard();
        //int randIndex2 = GenerateIndexCard();
        //Player.SetCard(_cards.Cards[randIndex], _cards.Cards[randIndex2]);

        //Table.AddCard(_cards.Cards[GenerateIndexCard()]);
        //Test();
        StartGame();
    }

    public Texture2D GetTexture (string name)
    {
        string imagePath = "шлях_до_зображення";
        Texture2D texture = new Texture2D(2, 2);
        byte[] imageData = System.IO.File.ReadAllBytes(imagePath);
        texture.LoadImage(imageData);
        return texture;
    }

    public int GenerateIndexCard ()
    {
        int randIndex;
        while (true)
        {
            randIndex = Random.Range(0, _cards.Cards.Count - 1);
            var usedIndex = _usedIndexCard.Contains(randIndex);
            if (!usedIndex)
            {
                _usedIndexCard.Add(randIndex);
                break;
            }
        }
        return randIndex;
    }

    public void StartGame ()
    {
        isFinish = false;
        Rate.ResetRate();       
        Table.ResetTable();
        Player.ResetPlayer();
        _showGameInfo.HideGameInfoState();

        foreach (var card in Table.CardsOnTable)
        {
            if (!card.CardFree)
                Debug.Log(card.CardInfo.Name + " : " + card.CardInfo.Ordinal);
        }

        foreach (var card in Player.PlayerCard)
        {
            if (!card.CardFree)
                Debug.Log(card.CardInfo.Name + " : " + card.CardInfo.Ordinal);
        }

        _usedIndexCard = new List<int>();

        int randIndex = GenerateIndexCard();
        int randIndex2 = GenerateIndexCard();
        Player.SetCard(_cards.Cards[randIndex], _cards.Cards[randIndex2]);

        Table.AddCard(_cards.Cards[GenerateIndexCard()]);
    }

    public void StartNewGame ()
    {
        if (!isFinish)
            Player.Money -= Rate.Scope;
        StartGame();
    }

    public void AddCardToTable ()
    {
        if (Table.CardHasSlotFree && !Rate.Click)
        {
            Table.AddCard(_cards.Cards[GenerateIndexCard()]);
            Rate.Click = true;
        }
            

        if (!Table.CardHasSlotFree && !isFinish)
        {
            isFinish = true;
            string gameState = StateWin.CheckWiner(Table.CardsOnTable, Player, Rate);
            _showGameInfo.ShowGameInfoState(gameState);
        }
    }

    private void Test ()
    {
        int cardOrdinal = 4;
        CardInfo forPlayerFirst = Cards.Find(card => card.Ordinal == cardOrdinal);
        CardInfo forPlayerSecond = Cards.Find(card => card.Ordinal == 9);
        CardInfo forTableFirst = Cards.Find(card => card.Ordinal == cardOrdinal);
        CardInfo forTableSecond = Cards.Find(card => card.Ordinal == cardOrdinal);

        Player.SetCard(forPlayerFirst, forPlayerSecond);
        Table.AddCard(forTableSecond);
        Table.AddCard(forTableFirst);
    }
}

