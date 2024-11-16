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
}

public class TaskManager : MonoBehaviour
{
    public List<Task> tasks; // Görevlerin listesi

    public void CollectStar(int taskIndex)
    {
        if (!tasks[taskIndex].isCompleted && tasks[taskIndex].starsCollected < tasks[taskIndex].starsRequired)
        {
            tasks[taskIndex].starsCollected++;
            if (tasks[taskIndex].starsCollected >= tasks[taskIndex].starsRequired)
            {
                tasks[taskIndex].isCompleted = true;
                Debug.Log(tasks[taskIndex].taskName + " tamamlandı!");
            }
        }
    }
}
