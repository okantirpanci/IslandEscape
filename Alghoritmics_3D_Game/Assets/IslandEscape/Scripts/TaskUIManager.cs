using UnityEngine;
using UnityEngine.UI;

public class TaskUIManager : MonoBehaviour
{
    public TaskManager taskManager;      // TaskManager referansı
    public GameObject taskDetailPanel;  // Görev detay paneli
    public Text taskTitle;              // Görev adı
    public Text taskDescription;        // Görev açıklaması
    public Text taskProgress;           // Görev ilerlemesi
    public Transform contentTransform;  // Scroll View'in Content alanı
    public GameObject taskPrefab;       // Görev Prefab'i

    private int currentTaskIndex;       // Seçilen görevin indeksi

    private void Start()
    {
        taskDetailPanel.SetActive(false); // Oyun başında paneli gizle
        CreateTaskList(); // Görev listesini oluştur
    }

    public void CreateTaskList()
    {
        foreach (Task task in taskManager.tasks)
        {
            GameObject newTask = Instantiate(taskPrefab, contentTransform); // Görevi oluştur
            TaskItem taskItem = newTask.GetComponent<TaskItem>();

            int index = taskManager.tasks.IndexOf(task); // Görevin indeksini al
            taskItem.taskIndex = index; // TaskItem'deki indeks değerini ayarla

            // Görev adını yazdır
            taskItem.taskNameButton.GetComponentInChildren<Text>().text = task.taskName;

            // CompleteButton'ın durumu görev tamamlanma durumuna göre ayarlanır
            taskItem.completeButton.interactable = task.isCompleted;
        }
    }

    public void DisplayTaskDetails(int taskIndex)
    {
        currentTaskIndex = taskIndex; // Seçilen görev indeksini kaydet

        // Görevi al
        Task task = taskManager.tasks[taskIndex];

        // UI elemanlarını doldur
        taskTitle.text = task.taskName; // Görev adını yazdır
        taskDescription.text = task.description; // Görev açıklamasını yazdır
        taskProgress.text = task.starsCollected + " / " + task.starsRequired; // İlerleme durumunu yazdır
    }

    public void CompleteTask(int taskIndex)
    {
        Task task = taskManager.tasks[taskIndex]; // Mevcut görev bilgilerini al
        if (!task.isCompleted)
        {
            task.isCompleted = true;
            Debug.Log("Görev tamamlandı: " + task.taskName);
        }
        else
        {
            Debug.Log("Görev zaten tamamlanmış: " + task.taskName);
        }

        // Tamamlama durumu güncelleniyor
        CreateTaskList();
    }
}
