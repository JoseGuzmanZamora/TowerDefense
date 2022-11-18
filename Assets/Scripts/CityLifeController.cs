using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityLifeController : MonoBehaviour
{
    public int hitAmount = 0;
    public TextMeshProUGUI hitCountText;

    private void Update() {
        hitCountText.SetText($"Hit Count: {hitAmount.ToString()}");
    }
}
