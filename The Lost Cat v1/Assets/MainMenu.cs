using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

   public void ExitGame()
   {
      Debug.Log("Exiting game...");
      Application.Quit();
   }

   // public void Resume()
   //  {
   //      pauseGameMenu.SetActive(false);
   //      Time.timeScale = 1f;
   //      PauseGame = false;
   //  }
}
