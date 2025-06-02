using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DeathObject : MonoBehaviour
{
    private string savePath => Application.persistentDataPath + "/scene_save.json";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.CompareTag("Player"))
            NewGame();

    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
            NewGame();
    }

    public void NewGame()
    {
        // Удалить старое сохранение (если есть)
        if (System.IO.File.Exists(Application.persistentDataPath + "/scene_save.json"))
            System.IO.File.Delete(Application.persistentDataPath + "/scene_save.json");

        // Загрузить интро-сцену
        UnityEngine.SceneManagement.SceneManager.LoadScene(2); // имя твоей интро-сцены
    }
}
