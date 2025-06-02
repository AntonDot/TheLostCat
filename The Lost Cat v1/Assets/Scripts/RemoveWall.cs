using UnityEngine;

public class RemoveWall : MonoBehaviour
{
    [Header("Настройки исчезновения")]
    [SerializeField] private float fadeDistance = 3f;        // Расстояние, на котором начинается исчезновение
    [SerializeField] private float minDistance = 1f;         // Расстояние, на котором объект полностью прозрачен
    [SerializeField] private float fadeSpeed = 2f;           // Скорость исчезновения
    [SerializeField] private Transform player;               // Ссылка на игрока
    [SerializeField] private bool affectCollider = false;    // Отключать ли коллайдер при полном исчезновении

    private SpriteRenderer spriteRenderer;
    private Collider2D wallCollider;
    private Color originalColor;
    private float currentAlpha = 1f;

    void Start()
    {
        // Получаем компоненты
        spriteRenderer = GetComponent<SpriteRenderer>();
        wallCollider = GetComponent<Collider2D>();

        // Сохраняем исходный цвет и прозрачность
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

        // Если игрок не назначен, пытаемся найти его по тегу
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // Минимальное расстояние не должно превышать общее расстояние исчезновения
        if (minDistance > fadeDistance)
        {
            minDistance = fadeDistance;
        }
    }

    void Update()
    {
        if (player == null || spriteRenderer == null)
            return;

        // Рассчитываем расстояние до игрока
        float distance = Vector2.Distance(transform.position, player.position);

        // Если игрок находится в зоне исчезновения
        if (distance < fadeDistance)
        {
            // Рассчитываем новую прозрачность в диапазоне от minDistance до fadeDistance
            float targetAlpha;

            if (distance <= minDistance)
            {
                // Полностью прозрачный, если игрок ближе минимального расстояния
                targetAlpha = 0f;
            }
            else
            {
                // Линейная интерполяция от 0 до 1 в диапазоне расстояний
                targetAlpha = Mathf.Clamp01((distance - minDistance) / (fadeDistance - minDistance));
            }

            // Плавно меняем текущую прозрачность
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);

            // Применяем новый цвет с измененной прозрачностью
            Color newColor = originalColor;
            newColor.a = currentAlpha;
            spriteRenderer.color = newColor;

            // Если нужно влиять на коллайдер и объект почти исчез
            if (affectCollider && wallCollider != null)
            {
                wallCollider.enabled = currentAlpha > 0.1f;
            }
        }
        else if (currentAlpha < 1f)
        {
            // Восстанавливаем прозрачность, если игрок отошел
            currentAlpha = Mathf.Lerp(currentAlpha, 1f, Time.deltaTime * fadeSpeed);

            Color newColor = originalColor;
            newColor.a = currentAlpha;
            spriteRenderer.color = newColor;

            if (affectCollider && wallCollider != null && !wallCollider.enabled && currentAlpha > 0.1f)
            {
                wallCollider.enabled = true;
            }
        }
    }
}
