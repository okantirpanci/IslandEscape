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
        Task task = tasks[taskIndex];

        if (!task.isCompleted && task.starsCollected < task.starsRequired)
        {
            task.starsCollected++;
            Debug.Log($"CollectStar: {task.taskName}, StarsCollected: {task.starsCollected}, StarsRequired: {task.starsRequired}");

            if (task.starsCollected >= task.starsRequired)
            {
                task.isCompleted = false; // Görevi tamamlamış gibi görünmesini önlemek için geçici olarak false bırakıyoruz.
                Debug.Log($"Görev tamamlanmadı (test için): {task.taskName}");
            }

            // Görev güncelleme olayını tetikleyin
            OnTaskUpdated?.Invoke(taskIndex);
            Debug.Log($"OnTaskUpdated çağrıldı: TaskIndex {taskIndex}");
        }
        else
        {
            Debug.Log($"Görev zaten tamamlanmış veya yıldız sayısı maksimum: {task.taskName}");
        }
    }
}