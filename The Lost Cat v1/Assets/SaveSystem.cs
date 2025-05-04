using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SaveSystem : MonoBehaviour
{
    public FadePopup popup;
    public GameObject player; // Перетаскиваешь сюда игрока в Инспекторе

    public void SaveGame()
    {
        // Сохраняем позицию
        Vector3 pos = player.transform.position;
        PlayerPrefs.SetFloat("PlayerX", pos.x);
        PlayerPrefs.SetFloat("PlayerY", pos.y);
        PlayerPrefs.SetFloat("PlayerZ", pos.z);
        PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        Debug.Log($"Позиция сохранена: {pos}");

        // Показываем окно после кадра
        StartCoroutine(ShowPopupDelayed());
    }

    IEnumerator ShowPopupDelayed()
    {
        yield return new WaitForEndOfFrame(); // дождаться закрытия UI
        if (popup != null)
        {
            popup.ShowPopup();
        }
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerX"))
        {
            // Загружаем сохранённую сцену
            string sceneName = PlayerPrefs.GetString("SavedScene", SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("Сохранение не найдено.");
        }
    }

    // В этой функции применяем позицию после загрузки
    void Start()
    {
        if (player != null && PlayerPrefs.HasKey("PlayerX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerX");
            float y = PlayerPrefs.GetFloat("PlayerY");
            float z = PlayerPrefs.GetFloat("PlayerZ");

            player.transform.position = new Vector3(x, y, z);
            Debug.Log($"Позиция загружена: ({x}, {y}, {z})");
        }
    }

    public void NewGame()
    {
        // Удаляем сохранённую позицию и сцену
        PlayerPrefs.DeleteKey("PlayerX");
        PlayerPrefs.DeleteKey("PlayerY");
        PlayerPrefs.DeleteKey("PlayerZ");
        PlayerPrefs.DeleteKey("SavedScene");

        PlayerPrefs.Save();

        // Загружаем первую игровую сцену
        SceneManager.LoadScene("test"); // Укажи точное имя начальной сцены
    }

}
