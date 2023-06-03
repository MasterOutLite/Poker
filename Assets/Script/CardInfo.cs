using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "CardInfo")]
public class CardInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _ordinal;
    [SerializeField] private Suit _suit;
    [SerializeField] private Texture2D _texture;

     public string Name => _name;

    public int Ordinal => _ordinal;

    public Suit Suit => _suit;

    public Texture2D Texture => _texture;

}

public enum Suit
{
    Heart,
    Spade,
    Club,
    Diamond
}
