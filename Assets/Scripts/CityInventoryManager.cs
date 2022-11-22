using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityInventoryManager : MonoBehaviour
{
    public Dictionary<string, GameObject> defenseInventory = new Dictionary<string, GameObject>();
    // This defines if the defense is placed or not, "Available"
    public Dictionary<string, bool> defenseInventoryStatus = new Dictionary<string, bool>();

    public TextMeshProUGUI electricTurretAmountText;

    private void Update() {
        electricTurretAmountText.text = defenseInventoryStatus.Count.ToString();
    }
}
