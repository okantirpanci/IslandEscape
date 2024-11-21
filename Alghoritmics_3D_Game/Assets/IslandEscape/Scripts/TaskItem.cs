using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public int taskIndex;              // Görev indeksini tutar
    public Button taskNameButton;      // Görev adı butonu
    public Button completeButton;      // Görevi tamamlama butonu

    private bool rewardTaken = false;  // Ödülün alınıp alınmadığını kontrol eder

    private void Start()
    {
        // TaskName butonuna görev detaylarını açma işlevi ekle
        taskNameButton.onClick.AddListener(() =>
        {
            FindObjectOfType<TaskUIManager>().DisplayTaskDetails(taskIndex);
        });

        // CompleteButton'a görevi tamamlama işlevi ekle
        completeButton.onClick.AddListener(() =>
        {
            // Ödül henüz alınmamışsa işlemi gerçekleştir
            if (!rewardTaken)
            {
                FindObjectOfType<TaskUIManager>().CompleteTask(taskIndex);
                rewardTaken = true;                  // Ödül alındı olarak işaretle
                completeButton.interactable = false; // Butonu pasif hale getir
            }
            else
            {
                Debug.Log("Bu ödül zaten alındı!");
            }
        });

        // Görev durumuna göre butonun interaktifliğini ayarla
        Task task = FindObjectOfType<TaskManager>().tasks[taskIndex];
        completeButton.interactable = task.isCompleted && !rewardTaken; // Görev tamamlanmış ve ödül alınmamışsa aktif
    }
}
