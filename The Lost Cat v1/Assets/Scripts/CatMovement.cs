using UnityEngine;
using UnityEngine.InputSystem;

public class CatMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MoveSpeed;
    public Animator animator;
    float HorizontalMovement;
    bool isJump;
    float VerticalMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
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
         
        if (VerticalMovement > 0 && rb.linearVelocityY==0) 
        {
            isJump = true;
           
            rb.linearVelocity = new Vector2(rb.linearVelocityX , MoveSpeed*2);

        }
        
            animator.SetBool("IsWalking", true);
    }
    
}
