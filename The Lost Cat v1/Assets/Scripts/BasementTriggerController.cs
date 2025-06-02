using UnityEngine;

public class BasementTriggerController : MonoBehaviour
{
    public GameObject basementContainer;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowBasement();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideBasement();
        }
    }

    void ShowBasement()
    {
        basementContainer.SetActive(true);
    }

    void HideBasement()
    {
        basementContainer.SetActive(false);
    }
}