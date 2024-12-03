using UnityEngine;
using UnityEngine.UI;

public class TaskItem : MonoBehaviour
{
    public int taskIndex;              // Görev indeksini tutar
    public Button taskNameButton;      // Görev adı butonu
    public Button completeButton;      // Görevi tamamlama butonu

    private void Start()
    {
        taskNameButton.onClick.AddListener(() =>
        {
            FindObjectOfType<TaskUIManager>().DisplayTaskDetails(taskIndex);
        });

        completeButton.onClick.AddListener(() =>
        {
            Debug.Log($"CompleteButton tıklandı, taskIndex: {taskIndex}");
            FindObjectOfType<TaskUIManager>().CompleteTask(taskIndex);
        });

        Task task = FindObjectOfType<TaskManager>().tasks[taskIndex];
        completeButton.interactable = task.starsCollected >= task.starsRequired && !task.isCompleted;
        Debug.Log($"TaskItem başlangıç interaktif durumu: {completeButton.interactable}");
    }

}
