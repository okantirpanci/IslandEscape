using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject taskPanel; // Görev paneli referansı
    public GameObject taskDetailPanel; // Görev paneli referansı
    public KeyCode toggleKey = KeyCode.Tab; // Açma/kapatma tuşu (varsayılan: Tab)

    void Start()
    {
        // Oyun başladığında paneli gizli hale getir
        if (taskPanel != null && taskDetailPanel != null)
        {
            taskPanel.SetActive(false);
            taskDetailPanel.SetActive(false);
        }


    }

    void Update()
    {
        // ToggleKey'e basıldığında panelin aktiflik durumunu değiştir
        if (Input.GetKeyDown(toggleKey))
        {
            taskPanel.SetActive(!taskPanel.activeSelf);
            taskDetailPanel.SetActive(!taskDetailPanel.activeSelf);
        }
    }
}
