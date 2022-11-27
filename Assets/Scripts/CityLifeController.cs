using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityLifeController : MonoBehaviour
{
    public float hitThreshold = 0.5f;
    public int hitAmount = 0;
    public float cityLifePercentage = 100;
    public TextMeshProUGUI hitCountText;
    public UIController gameOverText;

    private void Update() {
        cityLifePercentage = Convert.ToInt32(100 - (hitAmount * hitThreshold));
        hitCountText.SetText($"City Life: {cityLifePercentage.ToString()}%");

        if (cityLifePercentage <= 0)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }
}
