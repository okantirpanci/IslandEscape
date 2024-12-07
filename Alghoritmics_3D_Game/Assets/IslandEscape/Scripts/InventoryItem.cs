using UnityEngine;

[System.Serializable]
public class InventoryItem : MonoBehaviour
{
    public string itemName; // Ödül adı
    public GameObject itemPrefab; // Oyun dünyasına yerleştirilecek model
    public Sprite itemIcon; // Envanterde görünecek ikon
}
