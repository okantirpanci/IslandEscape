using UnityEngine;

public class Tree : MonoBehaviour
{
    public int taskIndex; // Hangi g�reve ba�l� oldu�unu belirten indeks
    private int hit = 0; // A�ac�n ka� darbe ald���n� takip eder

    private void OnTriggerStay(Collider other)
    {
        // E�er oyuncunun baltas� a�aca �arp�yorsa
        if (other.CompareTag("Axe"))
        {
            // Oyuncunun "hit" de�eri 3'e ula�t�ysa i�lemi ger�ekle�tir
            if (PlayerController.hit == 3)
            {
                FindObjectOfType<TaskManager>().CollectStar(taskIndex); // G�revi tamamla
                Destroy(gameObject); // A�ac� yok et
                PlayerController.hit = 0; // Darbe sayac�n� s�f�rla
                Debug.Log(hit + " Tree");
            }
        }
    }
}
