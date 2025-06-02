using UnityEngine;

public class FishItem : MonoBehaviour
{
    public float pickupDistance = 1f; // Максимальная дистанция для подбора рыбы

    private Transform playerTransform;

    void Start()
    {
        // Находим игрока по тегу
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= pickupDistance)
        {
            QuestManager.Instance.CollectFish();
            Destroy(gameObject);
        }
    }
}
