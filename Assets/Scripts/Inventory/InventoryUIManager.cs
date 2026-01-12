using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;

    [Header("UI References")]
    public GameObject inventoryPanel;
    public TextMeshProUGUI inventoryText;

    private InventoryComponent _currentInventory;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Update()
    {
        // Close on Escape
        if (inventoryPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }

        // Update text while open
        if (inventoryPanel.activeSelf && _currentInventory != null)
        {
            RefreshText();
        }
    }

    public void Show(InventoryComponent inventory)
    {
        _currentInventory = inventory;
        inventoryPanel.SetActive(true);
        RefreshText();
    }

    public void Hide()
    {
        inventoryPanel.SetActive(false);
        _currentInventory = null;
    }

    private void RefreshText()
    {
        if (_currentInventory == null)
        {
            inventoryText.text = "No inventory.";
            return;
        }

        var inv = _currentInventory.inventory;
        if (inv.stacks.Count == 0)
        {
            inventoryText.text = "Empty.";
            return;
        }

        System.Text.StringBuilder sb = new();
        sb.AppendLine("Contents:");

        foreach (var stack in inv.stacks)
        {
            if (stack.item == null) continue;
            string name = string.IsNullOrEmpty(stack.item.displayName)
                ? stack.item.name
                : stack.item.displayName;

            sb.AppendLine($"{name}: {stack.amount}");
        }

        inventoryText.text = sb.ToString();
    }
}