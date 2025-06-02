using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("Состояние квеста")]
    public bool hasMetPenguin = false;
    public bool hasFish = false;
    public bool fishDelivered = false;

    [Header("Референсы")]
    public GameObject basementFish; // Рыба в подвале

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

    // Вызывается при подборе рыбы
    public void CollectFish()
    {
        hasFish = true;
        basementFish.SetActive(false);
        Debug.Log("Рыба собрана!");
    }

    // Вызывается при передаче рыбы пингвину
    public void DeliverFish()
    {
        if (hasFish)
        {
            hasFish = false;
            fishDelivered = true;
            Debug.Log("Рыба доставлена пингвину!");
        }
    }
}