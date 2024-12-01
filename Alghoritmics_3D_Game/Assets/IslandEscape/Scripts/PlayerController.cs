using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float trampolineBounceForce = 10f; // Trambolin zıplama kuvveti

    [Header("Camera Settings")]
    public Transform playerCamera;
    public float mouseSensitivity = 160f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;

    [Header("UI Settings")]
    public GameObject taskPanel; // Görev paneli
    private bool isTaskPanelOpen = false;

    private Rigidbody rb;
    private bool isGrounded;
    private float xRotation = 0f;
    private bool isBouncing = false; // Trambolinden zıplama durumu

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        LockCursor(true); // Oyunun başında fareyi kilitle
    }

    private void Update()
    {
        HandleTaskPanel(); // Görev paneli açma/kapatma

        if (isTaskPanelOpen) return; // Görev paneli açıkken hareket ve kamera devre dışı

        HandleMovement();  // Oyuncu hareketi
        HandleJump();      // Oyuncu zıplaması
        HandleCamera();    // Kamera kontrolü
    }

    private void HandleTaskPanel()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isTaskPanelOpen = !isTaskPanelOpen;
            taskPanel.SetActive(isTaskPanelOpen);
            LockCursor(!isTaskPanelOpen);
        }
    }

    private void HandleMovement()
    {
        // Hareket yönünü hesapla
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        // Dünya uzayından yerel uzaya dönüştür (oyuncunun yönüne göre hareket)
        Vector3 worldMove = transform.TransformDirection(moveDirection);

        // Hareketi Rigidbody'ye uygula
        rb.linearVelocity = new Vector3(worldMove.x * moveSpeed, rb.linearVelocity.y, worldMove.z * moveSpeed);
    }

    private void HandleJump()
    {
        GroundCheck(); // Oyuncunun yere değip değmediğini kontrol et

        if (Input.GetButtonDown("Jump") && isGrounded && !isBouncing)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void GroundCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundCheckDistance, groundLayer);
    }

    private void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Y ekseni dönüşü (kamerayı yukarı/aşağı hareket ettirme)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // X ekseni dönüşü (oyuncunun etrafında dönme)
        transform.Rotate(Vector3.up * mouseX);
    }

    private void LockCursor(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }

    private void OnDrawGizmos()
    {
        // GroundCheck alanını göstermek için
        Gizmos.color = Color.green;
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        Gizmos.DrawWireSphere(spherePosition, groundCheckDistance);
    }
}
