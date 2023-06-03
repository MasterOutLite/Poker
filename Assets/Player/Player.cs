using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Card> _playerCard;
    [SerializeField] private TMP_Text _textNickname;
    [SerializeField] private TMP_Text _textMoney;
    [SerializeField] private string _nickname;
    [SerializeField] private double _money;
    public List<Card> PlayerCard => _playerCard;
    public double Money
    {
        get => _money;
        set { _money = value; _textMoney.text = _money.ToString(); }
    }
    public string NickName
    {
        get => _nickname;
        set
        {
            _nickname = value;
            _textNickname.text = _nickname;
        }
    }

    public void SetCard (CardInfo cardsOne, CardInfo cardTwo)
    {
        if (cardsOne == null || cardTwo == null)
        {
            Debug.Log($"Error {gameObject.name}");
            throw new System.Exception($"Error one parametr is null. Error in {gameObject.name}");
        }

        _playerCard[0].SetCardInfo(cardsOne);
        _playerCard[1].SetCardInfo(cardTwo);
    }

    public void ResetPlayer ()
    {
        foreach (var card in _playerCard)
        {
            card.ResetCard();
        }
    }

    private void Awake ()
    {
        _textNickname.text = _nickname.ToString();
        _textMoney.text = _money.ToString();
    }
}
