using UnityEngine;

public class Star : MonoBehaviour
{
    public int taskIndex; // Hangi göreve bağlı olduğunu belirten indeks

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<TaskManager>().CollectStar(taskIndex);
            Destroy(gameObject); // Yıldızı yok et
        }
    }
}
