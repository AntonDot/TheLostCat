using UnityEngine;

public class FishItem : MonoBehaviour
{
    public float pickupDistance = 1f; // Максимальная дистанция для подбора рыбы

    private Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    private void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector2.Distance(transform.position, playerTransform.position);

        if (distance <= pickupDistance)
        {
            if (QuestManager.Instance != null)
            {
                // Только флаг и диалоги — без попытки деактивировать рыбу (она и так будет удалена)
                QuestManager.Instance.hasFish = true;
                QuestManager.Instance.UpdateDialogueTexts();
            }

            Destroy(gameObject);
        }
    }
}
