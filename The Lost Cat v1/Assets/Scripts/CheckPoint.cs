using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class CheckPoint : MonoBehaviour
{
    [Header("Настройки сохранения")]
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerVisual;

    private string savePath => Application.persistentDataPath + "/scene_save.json";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Player"))
            SaveGame();

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
}
