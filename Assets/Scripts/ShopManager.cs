using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public CityInventoryManager inventory;
    public CityEconomyManager economy;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyItem(string type)
    {
        if (type == DefenseTypes.ElectricTurret.ToString())
        {
            var price = 750;
            if (economy.moneyAmount >= price)
            {
                // is able to buy
                inventory.defenseInventoryStatus[Guid.NewGuid().ToString()] = false;
                economy.moneyAmount -= price;
            }
        }
    }

    public enum DefenseTypes
    {
        Default,
        ElectricTurret
    }
}
