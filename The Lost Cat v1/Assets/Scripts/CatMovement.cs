using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MoveSpeed;
    public Collider2D groundCheck;
    float HorizontalMovement;
    bool isJump;
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
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // Получаем все коллайдеры, касающиеся groundCheck
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        Collider2D[] colliders = new Collider2D[10];
        int numColliders = groundCheck.Overlap(contactFilter, colliders);

        for (int i = 0; i < numColliders; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                //if (!wasGrounded)
                //OnLandEvent.Invoke();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HorizontalMovement == 0)
        {
            animator.SetBool("IsWalking", false);
        }
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
            isJump = true;
           
            rb.linearVelocity = new Vector2(rb.linearVelocityX , MoveSpeed*1.6f);

        }
        
            animator.SetBool("IsWalking", true);
    }
    
}
