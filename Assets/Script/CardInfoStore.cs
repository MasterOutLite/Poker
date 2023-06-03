using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "CardInfoStore")]
public class CardInfoStore : ScriptableObject
{
    [SerializeField] private List<CardInfo> _cards;

    public List<CardInfo> Cards => _cards;
}
