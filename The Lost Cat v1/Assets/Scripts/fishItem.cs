using UnityEngine;

public class FishItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            QuestManager.Instance.CollectFish();
            Destroy(gameObject);
        }
    }
}