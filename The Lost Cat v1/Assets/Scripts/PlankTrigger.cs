using UnityEngine;

public class PlankTrigger : MonoBehaviour
{
    public GameObject invisibleWall; // ссылка на невидимую стену
    public Vector3 moveOffset = new Vector3(1f, 0, 0); // куда сдвинется дощечка
    public float moveSpeed = 2f;

    private bool isPlayerInRange = false;
    private bool isMoved = false;
    private Vector3 targetPosition;

    void Update()
    {
        if (isPlayerInRange && !isMoved && Input.GetKeyDown(KeyCode.E))
        {
            isMoved = true;
            targetPosition = transform.position + moveOffset;

            if (invisibleWall != null)
                invisibleWall.SetActive(false); // отключаем стену
        }

        // плавное движение дощечки
        if (isMoved && Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            isPlayerInRange = false;
    }
}
