using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    [SerializeField] private int startGold = 100;
    [SerializeField]private TextMeshProUGUI goldText;
    private static int _goldAmount = 0;

    private void Start()
    {
        _goldAmount = startGold;
        AdjustGoldText();
    }

    public static int Gold
    {
        get => _goldAmount;
        private set
        {
            _goldAmount = value;
        }
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        AdjustGoldText();
    }

    public void SubstractGold(int amount)
    {
        Gold -= amount;
        AdjustGoldText();
    }

    private void AdjustGoldText()
    {
        goldText.text = "GOLD: " + _goldAmount;
    }
    
}
