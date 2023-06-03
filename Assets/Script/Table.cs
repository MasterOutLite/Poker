using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] private List<Card> _cardsOnTable;
    public List<Card> CardsOnTable => _cardsOnTable;
    public bool CardHasSlotFree => _cardsOnTable.Find(Card => Card.CardFree);

    public void AddCard (CardInfo cardinfo)
    {
        Card card = _cardsOnTable.Find(cardFree => cardFree.CardFree);
        card.gameObject.SetActive(true);
        card.SetCardInfo(cardinfo);       
    }

    public void ResetTable ()
    {
        foreach (var card in _cardsOnTable)
        {
            card.ResetCard();
            card.gameObject.SetActive(false);
        }
    }

    private void Awake ()
    {
        foreach (Card card in _cardsOnTable)
        {
            card.gameObject.SetActive(false);
        }
    }
}
