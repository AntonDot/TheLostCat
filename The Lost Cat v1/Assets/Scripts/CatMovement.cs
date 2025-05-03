using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MoveSpeed;
    public Collider2D groundCheck;
    public Collider2D stairsCheck;
    float HorizontalMovement;
    float VerticalMovement;

    private Animator animator;
    private bool m_Grounded;
    private bool isJumping;
    private bool isClimbing;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Получаем все коллайдеры, касающиеся groundCheck
        if (groundCheck.IsTouchingLayers(LayerMask.GetMask("Ground")))
            m_Grounded = true;
        else
            m_Grounded = false;

        if (!stairsCheck.IsTouchingLayers(LayerMask.GetMask("Stairs")) && isClimbing)
        {
            isClimbing = false;
            animator.SetBool("IsClimbing", false);
            rb.gravityScale = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isClimbing)
        {
            rb.linearVelocityY = VerticalMovement * MoveSpeed;
            rb.gravityScale = 0;
        }
        if (HorizontalMovement == 0)
            animator.SetFloat("XVelocity", 0f);
        rb.linearVelocity = new Vector2(HorizontalMovement * MoveSpeed, rb.linearVelocityY);
        if (rb.linearVelocityY < 0 && m_Grounded == true && isJumping == true)
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }

    }

    public void Move(InputAction.CallbackContext context)
    {

        HorizontalMovement = context.ReadValue<Vector2>().x;
        VerticalMovement = context.ReadValue<Vector2>().y;
        Debug.Log(HorizontalMovement);
        if (HorizontalMovement < 0)
        {
            HorizontalMovement = -1;
            Vector2 rotate = transform.eulerAngles;
            rotate.y = 180;
            transform.rotation = Quaternion.Euler(rotate);
        }
        else if (HorizontalMovement > 0)
        {
            HorizontalMovement = 1;
            Vector2 rotate = transform.eulerAngles;
            rotate.y = 0;
            transform.rotation = Quaternion.Euler(rotate);
        }

        if (VerticalMovement > 0 && m_Grounded && !isClimbing)
        {

            if (stairsCheck.IsTouchingLayers(LayerMask.GetMask("Stairs")))
            {
                isClimbing = true;
                animator.SetBool("IsClimbing", true);
            }
            else
            {
                rb.linearVelocity = new Vector2(rb.linearVelocityX, MoveSpeed * 1.6f);
                m_Grounded = false;
                isJumping = true;
                animator.SetBool("IsJumping", true);
            }

        }
        animator.SetFloat("XVelocity", 1f);
    }

}
