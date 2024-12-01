using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>(); // Envanterdeki ödüller
    public Transform inventoryUIParent; // Envanter UI'nin içeriği (scrollview content)
    public GameObject inventorySlotPrefab; // Envanterde kullanılacak slot prefab'ı
    public Transform playerTransform; // Oyuncu referansı

    public void AddItemToInventory(InventoryItem item)
    {
        if (item == null)
        {
            Debug.LogError("Envantere eklenmek istenen item null!");
            return;
        }

        Debug.Log($"AddItemToInventory çağrıldı: {item.itemName}");

        inventory.Add(item); // Envantere ekle
        Debug.Log($"Envantere eklendi: {item.itemName}");

        // Envanter UI'ye yeni bir slot ekle
        GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryUIParent);
        InventorySlot slot = newSlot.GetComponent<InventorySlot>();

        if (slot != null)
        {
            Debug.Log("Slot başarıyla oluşturuldu.");
            slot.Initialize(item, playerTransform); // Slotu başlat ve player referansını geçir
        }
        else
        {
            Debug.LogError("Slot oluşturulamadı! InventorySlot script'i atanmış mı?");
        }
    }
}
