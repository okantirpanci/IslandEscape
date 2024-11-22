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

    public void UpdateTaskUI(int taskIndex)
    {
        Task task = taskManager.tasks[taskIndex];
        Debug.Log($"UpdateTaskUI: Task {task.taskName}, Completed: {task.isCompleted}, Stars: {task.starsCollected}/{task.starsRequired}");

        // Eğer görev detay paneli açık ve bu görev detay panelindeyse
        if (currentTaskIndex == taskIndex)
        {
            taskProgress.text = $"{task.starsCollected} / {task.starsRequired}";

            // Görev tamamlanma durumuna göre "Ödül Al" butonunu aktif/pasif yap
            TaskItem taskItem = contentTransform.GetChild(taskIndex).GetComponent<TaskItem>();
            if (taskItem != null)
            {
                bool canComplete = task.starsCollected >= task.starsRequired && !task.isCompleted;
                taskItem.completeButton.interactable = canComplete;
                Debug.Log($"CompleteButton güncellendi: Interactable = {canComplete}, StarsCollected = {task.starsCollected}, IsCompleted = {task.isCompleted}");
            }
        }

        // Scroll View içindeki görev listesindeki durumu güncelle
        TaskItem updatedTaskItem = contentTransform.GetChild(taskIndex).GetComponent<TaskItem>();
        if (updatedTaskItem != null)
        {
            bool canComplete = task.starsCollected >= task.starsRequired && !task.isCompleted;
            updatedTaskItem.completeButton.interactable = canComplete;
            Debug.Log($"UpdatedTaskItem CompleteButton güncellendi: Interactable = {canComplete}");
        }
    }






    public void CompleteTask(int taskIndex)
    {
        Task task = taskManager.tasks[taskIndex];
        Debug.Log($"CompleteTask çalıştı, TaskIndex: {taskIndex}, TaskName: {task.taskName}, IsCompleted: {task.isCompleted}, StarsCollected: {task.starsCollected}, StarsRequired: {task.starsRequired}");

        if (!task.isCompleted && task.starsCollected >= task.starsRequired)
        {
            task.isCompleted = true;
            Debug.Log($"Görev tamamlandı: {task.taskName}");

            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null && task.reward != null)
            {
                inventoryManager.AddItemToInventory(task.reward);
                Debug.Log($"Ödül envantere eklendi: {task.reward.itemName}");
            }
            else
            {
                Debug.Log("InventoryManager veya ödül bulunamadı.");
            }

            UpdateTaskUI(taskIndex);
        }
        else
        {
            Debug.Log($"Görev zaten tamamlanmış veya yıldız sayısı yetersiz: {task.taskName}");
        }
    }




}
