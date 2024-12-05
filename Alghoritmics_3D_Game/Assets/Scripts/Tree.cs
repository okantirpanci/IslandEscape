using UnityEngine;

public class Tree : MonoBehaviour
{
    public int taskIndex; // Hangi göreve baðlý olduðunu belirten indeks
    private int hit = 0; // Aðacýn kaç darbe aldýðýný takip eder

    private void OnTriggerStay(Collider other)
    {
        // Eðer oyuncunun baltasý aðaca çarpýyorsa
        if (other.CompareTag("Axe"))
        {
            // Oyuncunun "hit" deðeri 3'e ulaþtýysa iþlemi gerçekleþtir
            if (PlayerController.hit == 3)
            {
                FindObjectOfType<TaskManager>().CollectStar(taskIndex); // Görevi tamamla
                Destroy(gameObject); // Aðacý yok et
                PlayerController.hit = 0; // Darbe sayacýný sýfýrla
                Debug.Log(hit + " Tree");
            }
        }
    }
}
