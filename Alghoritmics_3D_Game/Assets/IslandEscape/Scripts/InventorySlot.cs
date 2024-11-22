using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemIcon; // Ödül ikonunu göstermek için
    private InventoryItem item;

    public void Initialize(InventoryItem newItem)
    {
        item = newItem;
        if (itemIcon != null && item.itemIcon != null)
        {
            Debug.Log($"Slot ikonu ayarlanıyor: {item.itemName}");
            itemIcon.sprite = item.itemIcon;
        }
        else
        {
            Debug.LogError("Slot ikonu ayarlanamadı. Eksik referans!");
        }
    }


    public void OnClick()
    {
        Debug.Log("Ödül seçildi: " + item.itemName);

        // Oyun alanına ödül ekleme
        if (item.itemPrefab != null)
        {
            Instantiate(item.itemPrefab, Vector3.zero, Quaternion.identity); // Ödülü oyun alanına ekle
        }
    }
}
