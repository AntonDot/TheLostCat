using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoadSignController : MonoBehaviour
{
    [TextArea]
    public string hintText; // Текст подсказки для этого знака

    private TMP_Text uiText;
    private CanvasGroup canvasGroup;

    void Start()
    {
        uiText = GetComponentInChildren<TMP_Text>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();

        uiText.text = hintText;
        HideHint();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter: " + other.name);
        if (other.CompareTag("Player"))
        {
            ShowHint();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit: " + other.name);
        if (other.CompareTag("Player"))
        {
            HideHint();
        }
    }

    [SerializeField] private float fadeSpeed = 3f;

    void ShowHint()
    {
        StopAllCoroutines();
        StartCoroutine(FadeHint(1f));
    }

    void HideHint()
    {
        StopAllCoroutines();
        StartCoroutine(FadeHint(0f));
    }

    IEnumerator FadeHint(float targetAlpha)
    {
        while (!Mathf.Approximately(canvasGroup.alpha, targetAlpha))
        {
            canvasGroup.alpha = Mathf.MoveTowards(
                canvasGroup.alpha,
                targetAlpha,
                fadeSpeed * Time.deltaTime
            );
            yield return null;
        }
    }
}