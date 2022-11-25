using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CityInventoryManager : MonoBehaviour
{
    public Dictionary<string, PositionDefense> spawnedInventory = new Dictionary<string, PositionDefense>();
    public int totalInventory = 0;
    public int availableInventory = 0;
    // This defines if the defense is placed or not, "Available"

    public TextMeshProUGUI electricTurretAmountText;
    public TextMeshProUGUI electricTurrentAmountTextUI;
    public TextMeshProUGUI electricTurrentAvailableAmount;

    private void Update() {
        availableInventory = totalInventory - spawnedInventory.Where(s => s.Value.isPositioned).ToList().Count;
        electricTurretAmountText.text = totalInventory.ToString();
        electricTurrentAmountTextUI.text = $"Total: {electricTurretAmountText.text}";
        electricTurrentAvailableAmount.text = $"Available: {availableInventory}";
    }
}
