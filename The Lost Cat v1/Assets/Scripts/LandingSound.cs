using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LandingSound : MonoBehaviour
{
    public LayerMask groundLayer;
    public float checkDistance = 0.1f;
    public AudioClip landClip;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private bool wasGrounded;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        wasGrounded = IsGrounded();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();

        if (!wasGrounded && isGrounded && rb.linearVelocity.y <= 0.1f)
        {
            // Приземление
            if (landClip != null)
            {
                audioSource.PlayOneShot(landClip);
            }
        }

        wasGrounded = isGrounded;
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);
        return hit.collider != null;
    }
}
