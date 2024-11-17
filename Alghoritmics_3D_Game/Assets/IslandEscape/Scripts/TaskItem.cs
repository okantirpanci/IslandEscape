using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public int taskIndex;              // Görev indeksini tutar
    public Button taskNameButton;      // Görev adı butonu
    public Button completeButton;      // Görevi tamamlama butonu

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
            FindObjectOfType<TaskUIManager>().CompleteTask(taskIndex);
        });

        // Görev durumuna göre butonun interaktifliğini ayarla
        Task task = FindObjectOfType<TaskManager>().tasks[taskIndex];
        completeButton.interactable = task.isCompleted;
    }
}
