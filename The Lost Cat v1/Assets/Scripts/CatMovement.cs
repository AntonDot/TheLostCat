using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MoveSpeed;
    public Collider2D groundCheck;
    float HorizontalMovement;
    float VerticalMovement;

    private Animator animator;
    private bool m_Grounded;
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
    }


    // Update is called once per frame
    void Update()
    {
        if (HorizontalMovement == 0)
            animator.SetBool("IsWalking", false);
        rb.linearVelocity = new Vector2(HorizontalMovement * MoveSpeed, rb.linearVelocityY);
    }

    public void Move(InputAction.CallbackContext context)
    {

        HorizontalMovement = context.ReadValue<Vector2>().x;
        VerticalMovement = context.ReadValue<Vector2>().y;
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

        if (VerticalMovement > 0 && m_Grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, MoveSpeed * 1.6f);
            m_Grounded = false;
        }

        animator.SetBool("IsWalking", true);
    }

}
