using UnityEngine;
using UnityEngine.UI;

public class TaskUIManager : MonoBehaviour
{
    public TaskManager taskManager;        // TaskManager referansı
    public GameObject taskDetailPanel;    // Görev detay paneli
    public Text taskTitle;                // Görev adı
    public Text taskDescription;          // Görev açıklaması
    public Text taskProgress;             // Görev ilerlemesi
    public Transform contentTransform;    // Scroll View'in Content alanı
    public GameObject taskPrefab;         // Görev Prefab'i

    private int currentTaskIndex = -1;    // Seçilen görevin indeksi (başlangıçta seçilmemiş)

    private void Start()
    {
        taskDetailPanel.SetActive(false); // Oyun başında paneli gizle
        CreateTaskList();                 // Görev listesini oluştur

        // TaskManager'daki OnTaskUpdated olayına abone ol
        taskManager.OnTaskUpdated += UpdateTaskUI;
    }

    private void OnDestroy()
    {
        // TaskManager olayından çık
        taskManager.OnTaskUpdated -= UpdateTaskUI;
    }

    public void CreateTaskList()
    {
        // Content altındaki tüm mevcut görevleri temizle
        foreach (Transform child in contentTransform)
        {
            Destroy(child.gameObject);
        }

        // Yeni görevler oluştur
        for (int i = 0; i < taskManager.tasks.Count; i++)
        {
            Task task = taskManager.tasks[i];
            GameObject newTask = Instantiate(taskPrefab, contentTransform); // Görevi oluştur
            TaskItem taskItem = newTask.GetComponent<TaskItem>();
            taskItem.taskIndex = i;
            taskItem.taskNameButton.GetComponentInChildren<Text>().text = task.taskName;
            taskItem.completeButton.interactable = task.isCompleted;
        }
    }

    public void DisplayTaskDetails(int taskIndex)
    {
        currentTaskIndex = taskIndex; // Seçilen görev indeksini kaydet

        // Görevi al
        Task task = taskManager.tasks[taskIndex];

        // Detay panelini güncelle ve göster
        taskDetailPanel.SetActive(true);
        taskTitle.text = task.taskName;
        taskDescription.text = task.description;
        taskProgress.text = task.starsCollected + " / " + task.starsRequired;

        // Tamamlandı butonunun durumunu kontrol et
        TaskItem taskItem = contentTransform.GetChild(taskIndex).GetComponent<TaskItem>();
        taskItem.completeButton.interactable = task.isCompleted;
    }

    private void UpdateTaskUI(int taskIndex)
    {
        Task task = taskManager.tasks[taskIndex];

        // Eğer detay paneli açık ve güncellenen görev bu ise
        if (currentTaskIndex == taskIndex)
        {
            taskProgress.text = task.starsCollected + " / " + task.starsRequired;

            // Görev tamamlandıysa "Tamamlandı" butonunu aktif hale getir
            TaskItem taskItem = contentTransform.GetChild(taskIndex).GetComponent<TaskItem>();
            taskItem.completeButton.interactable = task.isCompleted;
        }

        // Scroll View'deki görev listesini güncelle
        TaskItem updatedTaskItem = contentTransform.GetChild(taskIndex).GetComponent<TaskItem>();
        updatedTaskItem.completeButton.interactable = task.isCompleted;
        updatedTaskItem.taskNameButton.GetComponentInChildren<Text>().text = task.taskName + (task.isCompleted ? " (Tamamlandı)" : "");
    }

    public void CompleteTask(int taskIndex)
    {
        Task task = taskManager.tasks[taskIndex]; // Mevcut görev bilgilerini al
        if (!task.isCompleted)
        {
            task.isCompleted = true;
            Debug.Log("Görev tamamlandı: " + task.taskName);

            // Görev ödülünü envantere eklemek için InventoryManager çağrısı
            if (task.reward != null)
            {
                FindObjectOfType<InventoryManager>().AddItemToInventory(task.reward);
            }

            // Görev detaylarını güncelle
            UpdateTaskUI(taskIndex);
        }
        else
        {
            Debug.Log("Görev zaten tamamlanmış: " + task.taskName);
        }
    }
}
