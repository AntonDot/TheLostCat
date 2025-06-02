using UnityEngine;

public class PenguinCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем, что столкнулся игрок и у него есть рыба
        if (collision.CompareTag("Player") && QuestManager.Instance.hasFish)
        {
            // Вызываем метод доставки рыбы
            QuestManager.Instance.DeliverFish();
        }
    }
}