using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelController : MonoBehaviour
{
    public GameObject endCanvas; // Canvas для показа концовки
    public Button mainMenuButton; // Кнопка для возврата в меню
    public string mainMenuSceneName = "MainMenu"; // Имя сцены главного меню

    void Start()
    {
        if (endCanvas != null)
            endCanvas.SetActive(false); // Скрываем Canvas в начале

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Проверка тега игрока
        {
            if (endCanvas != null)
            {
                endCanvas.SetActive(true); // Показываем Canvas концовки
                Time.timeScale = 0f; // Останавливаем время (по желанию)
            }
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Возобновляем время
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
