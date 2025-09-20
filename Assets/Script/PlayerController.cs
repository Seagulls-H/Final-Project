using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;     // tốc độ di chuyển ngang
    public float jumpForce = 6f;    // lực nhảy

    [Header("Ground Check")]
    public Transform groundCheck;    // Empty đặt dưới chân player
    public float groundCheckRadius = 0.12f;
    public LayerMask groundLayer;    // Layer cho nền đất

    private Rigidbody2D rb;
    private float moveInput;
    private bool isGrounded;
    private bool facingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- Lấy input trái/phải (A/D hoặc mũi tên) ---
        moveInput = Input.GetAxisRaw("Horizontal");

        // --- Nhảy: chỉ nhảy khi chạm đất ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // --- Đổi hướng nhân vật khi di chuyển ---
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();
    }

    void FixedUpdate()
    {
        // --- Kiểm tra player có chạm đất hay không ---
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // --- Di chuyển ngang ---
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 s = transform.localScale;
        s.x *= -1;                   // nhân -1 để lật trục X
        transform.localScale = s;
    }

    // --- ⚠ Tạm thời bỏ phần Hazard / chết để test di chuyển ---
    // void OnCollisionEnter2D(Collision2D collision) { ... }
    // void OnTriggerEnter2D(Collider2D other) { ... }
}
