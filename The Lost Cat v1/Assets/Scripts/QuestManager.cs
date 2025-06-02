using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Состояние квеста")]
    public bool hasMetPenguin = false;
    public bool hasFish = false;
    public bool fishDelivered = false;

    [Header("Референсы")]
    public GameObject basementFish; // Рыба в подвале
    public FloatingDialogue penguinDialogue; // Ссылка на скрипт диалога пингвина
    public GameObject wall; // Стена, которую нужно разрушить

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
    public string fishCollectedDialogue = "О, я вижу у тебя рыбка! Неси её сюда скорее!";
    [TextArea(3, 10)]
    public string afterFishDialogue = "Хехе, спасибо за помощь, а я тебя нае*ал";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Инициализация начальных диалогов
        UpdateDialogueTexts();
    }

    // Обновляет тексты диалогов в зависимости от состояния квеста
    public void UpdateDialogueTexts()
    {
        if (penguinDialogue == null) return;

        List<string> dialogueList = new List<string>();

        if (fishDelivered)
        {
            // После доставки рыбы
            dialogueList.Add(afterFishDialogue);
        }
        else if (hasFish)
        {
            // Игрок несёт рыбу
            dialogueList.Add(fishCollectedDialogue);
        }
        else if (!hasMetPenguin)
        {
            // Первая встреча
            dialogueList.Add(firstMeeting1);
            dialogueList.Add(firstMeeting2);
        }
        else
        {
            // Повторная встреча
            dialogueList.Add(repeatDialogue1);
            dialogueList.Add(repeatDialogue2);
        }

        // Обновляем тексты в FloatingDialogue
        penguinDialogue.dialogueTexts = dialogueList;
        penguinDialogue.ResetDialogue();
    }

    // Вызывается при подборе рыбы
    public void CollectFish()
    {
        hasFish = true;
        basementFish.SetActive(false);
        Debug.Log("Рыба собрана!");
        UpdateDialogueTexts();
    }

    // Вызывается при передаче рыбы пингвину
    public void DeliverFish()
    {
        if (hasFish)
        {
            hasFish = false;
            fishDelivered = true;
            Debug.Log("Рыба доставлена пингвину!");
            wall.SetActive(false); // Убираем стену
            UpdateDialogueTexts();
        }
    }

    // Вызывается при первой встрече с пингвином
    public void MeetPenguin()
    {
        if (!hasMetPenguin)
        {
            hasMetPenguin = true;
            Debug.Log("Первая встреча с пингвином!");
            UpdateDialogueTexts();
        }
    }
}
