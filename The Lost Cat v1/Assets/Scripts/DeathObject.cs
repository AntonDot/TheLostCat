using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathObject : MonoBehaviour
{
    private string savePath => Application.persistentDataPath + "/scene_save.json";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Player"))
            LoadSavedScene();

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
            LoadSavedScene();
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
        Debug.Log("try load scene");
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
}
