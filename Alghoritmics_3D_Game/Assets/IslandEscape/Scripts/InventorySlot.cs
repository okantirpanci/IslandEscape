using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemIcon; // Ödül ikonunu göstermek için
    private InventoryItem item;

    public void Initialize(InventoryItem newItem)
    {
        item = newItem;
        if (item.itemIcon != null)
        {
            itemIcon.sprite = item.itemIcon; // Ödül ikonunu slotta göster
            itemIcon.enabled = true; // İkonu görünür yap
        }
    }


    public void OnClick()
    {
        Debug.Log("Ödül seçildi: " + item.itemName);

        if (item.itemPrefab != null)
        {
            Instantiate(item.itemPrefab, Vector3.zero, Quaternion.identity); // Ödülü sıfır noktasına ekle
        }
    }

}

