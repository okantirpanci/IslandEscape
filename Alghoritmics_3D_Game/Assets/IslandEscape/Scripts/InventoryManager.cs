using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>(); // Envanterdeki ödüller
    public Transform inventoryUIParent; // Envanter UI'nin içeriği (scrollview content)
    public GameObject inventorySlotPrefab; // Envanterde kullanılacak slot prefab'ı

    // Ödül ekleme fonksiyonu
    public void AddItemToInventory(InventoryItem item)
    {
        inventory.Add(item); // Envantere ekle

        // Envanter UI'ye yeni bir slot ekle
        GameObject newSlot = Instantiate(inventorySlotPrefab, inventoryUIParent);
        InventorySlot slot = newSlot.GetComponent<InventorySlot>();
        slot.Initialize(item); // Slotu başlat
    }
}
