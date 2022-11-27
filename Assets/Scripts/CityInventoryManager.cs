using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CityInventoryManager : MonoBehaviour
{
    public List<PositionDefense> spawnedInventory = new List<PositionDefense>();
    public int totalInventory = 0;
    public int availableInventory = 0;
    // This defines if the defense is placed or not, "Available"

    public TextMeshProUGUI electricTurrentAmountTextUI;
    public TextMeshProUGUI electricTurrentAvailableAmount;

    private void Update() {
        availableInventory = totalInventory - spawnedInventory.Where(s => s.isPositioned).ToList().Count;
        electricTurrentAmountTextUI.text = $"Total: {totalInventory.ToString()}";
        electricTurrentAvailableAmount.text = $"Available: {availableInventory}";
    }
}
