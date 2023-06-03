using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private CardInfo _cardInfo;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private bool _held;

    public Image Image=> _image;
    public CardInfo CardInfo=> _cardInfo;
    public bool Held => _held;
    public bool CardFree => _cardInfo == null;

    public Card () { }

    public void Hold (bool? hold = null)
    {
        _held = hold.HasValue ? hold.Value : !_held;
    }

    public void SetCardInfo (CardInfo card)
    {
        _cardInfo = card;
        _image.sprite = Sprite.Create(card.Texture, 
            new Rect(0, 0, card.Texture.width, card.Texture.height),
            Vector2.one * 0.5f);
        _text.text = _cardInfo.Name;
    }

    public void ResetCard ()
    {
        _image.sprite = null;
        _text.text = "";
        _cardInfo = null;
        _image.color = _text.color = Color.white;
    }
}