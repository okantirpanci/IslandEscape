using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float jumpForce = 5f;

    [Header("Camera Settings")]
    public Transform playerCamera;
    public float mouseSensitivity = 160f;
    private float xRotation = 0f;

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;

    [Header("UI Settings")]
    public GameObject taskPanel; // Görev paneli referansı
    private bool isTaskPanelOpen = false; // Görev panelinin açık olup olmadığını takip eder

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        LockCursor(true); // Başlangıçta fareyi kilitle
    }

    private void Update()
    {
        // Görev panelini açma/kapatma
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isTaskPanelOpen = !isTaskPanelOpen;
            taskPanel.SetActive(isTaskPanelOpen);
            LockCursor(!isTaskPanelOpen); // Görev paneli açıkken fareyi serbest bırak
        }

        // Eğer görev paneli açık değilse hareketi ve kamerayı güncelle
        if (!isTaskPanelOpen)
        {
            Movement();
            CameraRotation();
        }
    }

    private void Movement()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }

    private void LockCursor(bool isLocked)
    {
        if (isLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
