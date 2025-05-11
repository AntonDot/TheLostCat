using UnityEngine;

public class BoatMovement : MonoBehaviour, ISaveable
{
    [SerializeField] private GameObject leftWall;
    private bool isMoving = false;
    private Vector2 finalPos;
    private const string BoatID = "Boat_1";

    void Start()
    {
        finalPos = new Vector2(transform.position.x + 4.75f, transform.position.y);
    }

    void Update()
    {
        if (isMoving)
        {
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

    // ---------- ISaveable реализация ----------

    public string GetID() => BoatID;

    public SaveData Save()
    {
        return new SaveData
        {
            id = GetID(),
            position = transform.position,
            customFloat = isMoving ? 1f : 0f // 1 = лодка уже начала движение
        };
    }

    public void Load(SaveData data)
    {
        if (data.id != GetID()) return;

        transform.position = data.position;
        isMoving = data.customFloat > 0.5f;
        leftWall.SetActive(isMoving);
    }
}
