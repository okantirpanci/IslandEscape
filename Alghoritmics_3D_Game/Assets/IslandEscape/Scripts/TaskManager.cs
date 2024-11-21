using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public string taskName;        // Görev adı
    public string description;     // Görev açıklaması
    public int starsRequired;      // Gerekli yıldız sayısı
    public int starsCollected;     // Toplanan yıldız sayısı
    public bool isCompleted;       // Görevin tamamlanma durumu

    // Görev ödülü
    public InventoryItem reward;   // Görev tamamlandığında verilecek ödül
}


public class TaskManager : MonoBehaviour
{
    public List<Task> tasks; // Görevlerin listesi

    // Görev güncelleme olayını tanımla
    public delegate void TaskUpdatedDelegate(int taskIndex);
    public event TaskUpdatedDelegate OnTaskUpdated;

    public void CollectStar(int taskIndex)
    {
        if (!tasks[taskIndex].isCompleted && tasks[taskIndex].starsCollected < tasks[taskIndex].starsRequired)
        {
            tasks[taskIndex].starsCollected++; // Yıldız sayısını artır

            // Görev tamamlandıysa
            if (tasks[taskIndex].starsCollected >= tasks[taskIndex].starsRequired)
            {
                tasks[taskIndex].isCompleted = true;
                Debug.Log(tasks[taskIndex].taskName + " tamamlandı!");
            }

            // Görev güncelleme olayını tetikle
            OnTaskUpdated?.Invoke(taskIndex);
        }
    }

}