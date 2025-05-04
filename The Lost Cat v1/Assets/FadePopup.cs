using UnityEngine;
using System.Collections;

public class FadePopup : MonoBehaviour
{
    public CanvasGroup popupGroup;
    public float fadeDuration = 0.5f;
    public float displayTime = 2f;

    void Start()
    {
        popupGroup.alpha = 0f;
        // Убираем отключение объекта! Он должен быть активен всегда.
    }

    public void ShowPopup()
    {
        StopAllCoroutines();
        StartCoroutine(FadeSequence());
    }

    IEnumerator FadeSequence()
    {
        // Fade in
        yield return StartCoroutine(Fade(0f, 1f));

        // Ждём заданное время
        yield return new WaitForSeconds(displayTime);

        // Fade out
        yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            popupGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, t / fadeDuration);
            yield return null;
        }
        popupGroup.alpha = endAlpha;
    }
}
