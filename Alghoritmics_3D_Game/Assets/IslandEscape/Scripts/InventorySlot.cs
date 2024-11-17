using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemIcon; // Ödül ikonunu göstermek için
    private InventoryItem item;

    public void Initialize(InventoryItem newItem)
    {
        item = newItem;
        itemIcon.sprite = item.itemIcon; // Ödül ikonunu slotta göster
    }

    public void OnClick()
    {
        Debug.Log("Ödül seçildi: " + item.itemName);
        // İleride ödülü oyun alanına yerleştirmek için kullanılabilir
    }
}
