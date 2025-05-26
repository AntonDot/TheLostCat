//@title IntroController.cs
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public Sprite[] slides;           // Картинки интро
    public Image displayImage;        // Ссылка на UI Image
    private int currentIndex = 0;
    private bool isSkipping = false;

    void Start()
    {
        if (slides.Length > 0)
            displayImage.sprite = slides[0];
    }

    void Update()
    {
        if (isSkipping) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipIntro();
        }
        else if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            ShowNextSlide();
        }
    }

    void ShowNextSlide()
    {
        currentIndex++;
        if (currentIndex < slides.Length)
        {
            displayImage.sprite = slides[currentIndex];
        }
        else
        {
            LoadNextScene();
        }
    }

    void SkipIntro()
    {
        isSkipping = true;
        LoadNextScene();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
