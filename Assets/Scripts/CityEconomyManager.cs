using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityEconomyManager : MonoBehaviour
{
    public int moneyAmount = 0;
    public TextMeshProUGUI moneyText;

    public void IncreaseMoneyAmount(int amount)
    {
        moneyAmount += amount;
    }

    public void DecreaseMoneyAmount(int amount)
    {
        moneyAmount -= amount;
    }

    private void Update() {
        if (moneyText is not null) moneyText.text = $"Budget: ${moneyAmount}";
    }
}
