using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>(); // Envanterdeki ödüller
    public Transform inventoryUIParent; // Envanter UI'nin içeriği (scrollview content)
    public GameObject inventorySlotPrefab; // Envanterde kullanılacak slot prefab'ı

    public void AddItemToInventory(InventoryItem item)
    {
        Debug.Log($"İtem İkonu Kontrolü: {item.itemName}, İkon: {item.itemIcon}");
        if (item.itemIcon == null)
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
            slot.Initialize(item); // Slotu başlat
        }
        else
        {
            Debug.LogError("Slot oluşturulamadı! InventorySlot script'i atanmış mı?");
        }
    }
}