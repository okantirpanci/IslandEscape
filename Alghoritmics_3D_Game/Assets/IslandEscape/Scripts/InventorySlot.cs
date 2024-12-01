using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image itemIcon; // Ödül ikonunu göstermek için
    private InventoryItem item;
    public Transform playerTransform; // Oyuncunun pozisyonunu almak için referans

    public void Initialize(InventoryItem newItem, Transform player)
    {
        item = newItem;
        playerTransform = player;

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

        // Oyuncunun pozisyonunda ödülü oluştur
        if (item.itemPrefab != null && playerTransform != null)
        {
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * 2; // Oyuncunun önünde spawn olsun
            Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity);
            Debug.Log($"Ödül oluşturuldu: {item.itemName} at {spawnPosition}");
        }
        else
        {
            Debug.LogError("Ödül oluşturulamadı! Prefab veya PlayerTransform eksik.");
        }
    }
}
