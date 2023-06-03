using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowGameInfo : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;

    public void ShowGameInfoState (string text)
    {
        gameObject.SetActive(true);
        _text.text = text;
    }

    public void HideGameInfoState ()
    {
        gameObject.SetActive(false);       
    }

    void Start ()
    {
        gameObject.SetActive(false);
    }
}
