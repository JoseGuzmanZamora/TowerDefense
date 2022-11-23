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

    private void Update() {
        cityLifePercentage = 100 - (hitAmount * hitThreshold);
        hitCountText.SetText($"City Life: {cityLifePercentage.ToString()}%");
    }
}
