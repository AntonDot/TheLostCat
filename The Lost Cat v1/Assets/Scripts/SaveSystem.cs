// SaveSystem.cs (JSON-подход + загрузка сцены только через кнопку "Продолжить")
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class SaveData
{
    public string id;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 cameraPosition;
    public float customFloat;
    public string savedScene;
}

[System.Serializable]
public class SceneSave
{
    public List<SaveData> savedObjects = new();
}

public interface ISaveable
{
    string GetID();
    SaveData Save();
    void Load(SaveData data);
}

public class SaveSystem : MonoBehaviour
{
    [Header("Настройки сохранения")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerVisual;

    private string savePath => Application.persistentDataPath + "/scene_save.json";

    private void Start()
    {
        if (File.Exists(savePath))
        {
            if (SceneManager.GetActiveScene().name != "MainMenu")
            {
                ApplySavedData(); // 👈 ВАЖНО: только здесь восстанавливаем позицию
            }
            else
            {
                Debug.Log("SaveSystem: JSON найден, но сцена MainMenu — ничего не загружаем.");
            }
        }
    }


    public void SaveGame()
    {
        SceneSave sceneSave = new SceneSave();
        var saveables = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        foreach (var s in saveables)
        {
            if (s is ISaveable saveable)
            {
                sceneSave.savedObjects.Add(saveable.Save());
            }
        }

        if (player != null)
        {
            SaveData playerData = new SaveData
            {
                id = "Player",
                position = player.transform.position,
                rotation = playerVisual != null ? playerVisual.eulerAngles : player.transform.eulerAngles,
                cameraPosition = Camera.main != null ? Camera.main.transform.position : Vector3.zero,
                savedScene = SceneManager.GetActiveScene().name
            };
            sceneSave.savedObjects.Add(playerData);
        }

        string json = JsonUtility.ToJson(sceneSave, true);
        File.WriteAllText(savePath, json);
        Debug.Log("SaveSystem: Сцена сохранена в JSON");
    }

    public void LoadSavedScene()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("SaveSystem: Нет файла сохранения.");
            return;
        }

        string json = File.ReadAllText(savePath);
        SceneSave sceneSave = JsonUtility.FromJson<SceneSave>(json);

        foreach (var saved in sceneSave.savedObjects)
        {
            if (!string.IsNullOrEmpty(saved.savedScene))
            {
                Debug.Log("SaveSystem: Загружается сцена: " + saved.savedScene);
                SceneManager.LoadScene(saved.savedScene);
                break;
            }
        }
    }

    public void ApplySavedData()
    {
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        SceneSave sceneSave = JsonUtility.FromJson<SceneSave>(json);

        var saveables = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);

        foreach (var s in saveables)
        {
            if (s is ISaveable saveable)
            {
                foreach (var saved in sceneSave.savedObjects)
                {
                    if (saveable.GetID() == saved.id)
                    {
                        saveable.Load(saved);
                        break;
                    }
                }
            }
        }

        foreach (var saved in sceneSave.savedObjects)
        {
            if (saved.id == "Player" && player != null)
            {
                player.transform.position = saved.position;

                if (playerVisual != null)
                    playerVisual.eulerAngles = saved.rotation;
                else
                    player.transform.eulerAngles = saved.rotation;

                if (Camera.main != null)
                {
                    Camera.main.transform.position = saved.cameraPosition;
                }
            }
        }

        Debug.Log("SaveSystem: Данные из JSON применены к текущей сцене.");
    }

    public void NewGame()
    {
        // Удалить старое сохранение (если есть)
        if (System.IO.File.Exists(Application.persistentDataPath + "/scene_save.json"))
            System.IO.File.Delete(Application.persistentDataPath + "/scene_save.json");

        // Загрузить интро-сцену
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); // имя твоей интро-сцены
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Player"))
            SaveGame();

    }

}
