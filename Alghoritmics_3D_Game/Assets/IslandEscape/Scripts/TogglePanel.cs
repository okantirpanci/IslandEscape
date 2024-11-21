using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject taskPanel;        // Görev paneli
    public GameObject taskDetailPanel; // Görev detay paneli
    public GameObject inventoryPanel;  // Envanter paneli
    public KeyCode toggleKey = KeyCode.Tab; // Açma/kapatma tuşu

    void Start()
    {
        // Oyun başladığında panelleri gizle
        if (taskPanel != null) taskPanel.SetActive(false);
        if (taskDetailPanel != null) taskDetailPanel.SetActive(false);
        if (inventoryPanel != null) inventoryPanel.SetActive(false);
    }

    void Update()
    {
        // Tab tuşuna basıldığında tüm panellerin durumunu değiştir
        if (Input.GetKeyDown(toggleKey))
        {
            bool isActive = taskPanel.activeSelf;
            taskPanel.SetActive(!isActive);
            taskDetailPanel.SetActive(!isActive);
            inventoryPanel.SetActive(!isActive);
        }
    }
}
