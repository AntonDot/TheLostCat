using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class FloatingDialogue : MonoBehaviour
{
    [Header("Настройки текста")]
    [TextArea(2, 5)]
    public List<string> dialogueTexts = new List<string>() {
        "Здорово, котяра, потерялся? У одного дома на заднем дворе есть подвал, а там рыба. Принеси мне её, а я тебе путь дальше покажу",
        "Сам-то я из подвала выбраться не смогу, а ты вон какой ловкий.",
        "Ну что, нашел рыбу? Принеси мне её, а я тебе путь дальше покажу",
        "Рыбка в подвале~~ рыбка в подвале~~"
    };

    [Header("Настройки отображения")]
    public float interactionDistance = 3f;
    public float textChangeInterval = 10f;
    public Vector3 textOffset = new Vector3(0, 2f, 0);

    [Header("Ссылки")]
    public GameObject textPrefab;

    private Transform playerTransform;
    private GameObject textInstance;
    private TextMeshPro textComponent;
    private bool isPlayerNearby = false;
    private int currentTextIndex = 0;
    private Coroutine textChangeCoroutine;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Создаем объект с текстом
        textInstance = Instantiate(textPrefab, transform.position + textOffset, Quaternion.identity);
        textInstance.transform.SetParent(transform);
        textComponent = textInstance.GetComponent<TextMeshPro>();
        if (textComponent == null)
        {
            textComponent = textInstance.AddComponent<TextMeshPro>();
        }

        // Настраиваем текст
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.fontSize = 5;

        // Скрываем текст изначально
        textInstance.SetActive(false);
    }

    void Update()
    {
        CheckPlayerDistance();

        // Текст всегда смотрит в сторону игрока
        if (isPlayerNearby && textInstance.activeSelf)
        {
            textInstance.transform.LookAt(new Vector3(playerTransform.position.x,
                                                     textInstance.transform.position.y,
                                                     playerTransform.position.z));
            textInstance.transform.Rotate(0, 180, 0);
        }
    }

    void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= interactionDistance)
        {
            if (!isPlayerNearby)
            {
                isPlayerNearby = true;
                ShowText();
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                isPlayerNearby = false;
                HideText();
            }
        }
    }

    void ShowText()
    {
        if (dialogueTexts.Count > 0)
        {
            textInstance.SetActive(true);
            textComponent.text = dialogueTexts[0];
            currentTextIndex = 0;

            if (textChangeCoroutine != null)
                StopCoroutine(textChangeCoroutine);

            textChangeCoroutine = StartCoroutine(ChangeTextPeriodically());
        }
    }

    void HideText()
    {
        textInstance.SetActive(false);

        if (textChangeCoroutine != null)
        {
            StopCoroutine(textChangeCoroutine);
            textChangeCoroutine = null;
        }
    }

    IEnumerator ChangeTextPeriodically()
    {
        while (isPlayerNearby)
        {
            yield return new WaitForSeconds(textChangeInterval);

            currentTextIndex = (currentTextIndex + 1) % dialogueTexts.Count;
            textComponent.text = dialogueTexts[currentTextIndex];
        }
    }

    // Визуализация радиуса взаимодействия в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);

        // Показываем позицию текста
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + textOffset, 0.1f);
    }

    // Добавьте этот метод в класс FloatingDialogue
    public void ResetDialogue()
    {
        // Сбрасываем индекс на начало списка
        currentTextIndex = 0;

        // Если текст активен, обновляем его
        if (textComponent != null && textInstance.activeSelf)
        {
            if (dialogueTexts.Count > 0)
                textComponent.text = dialogueTexts[0];
        }

        // Перезапускаем корутину смены текста
        if (isPlayerNearby)
        {
            if (textChangeCoroutine != null)
                StopCoroutine(textChangeCoroutine);

            textChangeCoroutine = StartCoroutine(ChangeTextPeriodically());
        }
    }

}
