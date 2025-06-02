using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PenguinDialogue : MonoBehaviour
{
    [Header("Диалоги")]
    [TextArea(3, 10)]
    public string firstMeeting1 = "Здорово, котяра, потерялся? У одного дома на заднем дворе есть подвал, а там рыба. Принеси мне её, а я тебе путь дальше покажу";
    [TextArea(3, 10)]
    public string firstMeeting2 = "Сам-то я из подвала выбраться не смогу, а ты вон какой ловкий.";
    [TextArea(3, 10)]
    public string repeatDialogue1 = "Ну что, нашел рыбу? Принеси мне её, а я тебе путь дальше покажу";
    [TextArea(3, 10)]
    public string repeatDialogue2 = "Рыбка в подвале~~ рыбка в подвале~~";
    [TextArea(3, 10)]
    public string afterFishDialogue = "Хехе, спасибо за помощь, а я тебя нае*ал";

    [Header("Настройки")]
    public float interactionDistance = 2f;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public float displayTimePerMessage = 5f;

    private Transform player;
    private bool isDialogueActive = false;
    private bool hasShownFirstDialogue = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (isDialogueActive) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionDistance)
        {
            StartCoroutine(ShowDialogue());
        }
    }

    IEnumerator ShowDialogue()
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);

        // Если рыба уже доставлена
        if (QuestManager.Instance.fishDelivered)
        {
            dialogueText.text = afterFishDialogue;
            yield return new WaitForSeconds(displayTimePerMessage);
        }
        // Если игрок несет рыбу
        else if (QuestManager.Instance.hasFish)
        {
            dialogueText.text = "О, принес рыбу! Дай-ка сюда!";
            yield return new WaitForSeconds(displayTimePerMessage);

            // Передача рыбы
            QuestManager.Instance.DeliverFish();

            dialogueText.text = afterFishDialogue;
            yield return new WaitForSeconds(displayTimePerMessage);
        }
        // Первая встреча
        else if (!QuestManager.Instance.hasMetPenguin)
        {
            dialogueText.text = firstMeeting1;
            yield return new WaitForSeconds(displayTimePerMessage);

            dialogueText.text = firstMeeting2;
            yield return new WaitForSeconds(displayTimePerMessage);

            QuestManager.Instance.hasMetPenguin = true;
            hasShownFirstDialogue = true;
        }
        // Повторный диалог
        else
        {
            dialogueText.text = repeatDialogue1;
            yield return new WaitForSeconds(displayTimePerMessage);
            dialogueText.text = repeatDialogue2;
            yield return new WaitForSeconds(displayTimePerMessage);
        }

        dialoguePanel.SetActive(false);
        isDialogueActive = false;
    }

    // Визуализация радиуса взаимодействия
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}