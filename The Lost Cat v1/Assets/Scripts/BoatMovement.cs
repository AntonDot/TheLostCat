using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    [SerializeField] private GameObject leftWall;
    private bool isMoving = false;
    private Vector2 finalPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalPos = new Vector2 (transform.position.x+4.75f, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) {
            var speed = 0.5f;
            var x = Mathf.Lerp(transform.position.x, finalPos.x, Time.deltaTime * speed);
            transform.position = new Vector3(x, transform.position.y);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Player")) 
            {
                isMoving = true;
                leftWall.SetActive(true);
            }
                
    }
}
