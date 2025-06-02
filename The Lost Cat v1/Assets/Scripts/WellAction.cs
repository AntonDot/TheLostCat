using System;
using UnityEngine;

public class WellAction : MonoBehaviour, ISaveable
{
    private bool isNear = false;
    private float speed = 0;
    [SerializeField] private GameObject water;
    private Vector2 finalPos;

    public bool isNotUsed = true;

    [Header("Аудио")]
    [SerializeField] private AudioSource waterSound;
    [SerializeField] private float startVolume = 0.2f;
    [SerializeField] private float idleVolume = 0.05f;
    private bool hasPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalPos = new Vector2 (water.transform.position.x,water.transform.position.y+1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E)) && isNear && isNotUsed)
        {

            speed = 1;

            if (!hasPlayed)
            {
                waterSound.volume = startVolume;
                waterSound.Play();
                hasPlayed = true;
            }
        }
        else
        {
            speed = 0;
            waterSound.Stop();
            hasPlayed = false;
        }

        var y = Mathf.Lerp(water.transform.position.y, finalPos.y, Time.deltaTime * speed);
        water.transform.position = new Vector2(finalPos.x, y);

        if (Math.Abs(water.transform.position.y - finalPos.y) < 0.01f)
        {
            // Переход к тихому звуку
            if (waterSound.isPlaying && waterSound.volume != idleVolume)
            {
                waterSound.volume = idleVolume;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            isNear = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isNear = false;
    }

    public string GetID() => "Well";
    public SaveData Save()
    {
        return new SaveData
        {
            id = GetID(),
            position = water.transform.position,
            customFloat = isNotUsed ? 1f : 0f,
        };
    }

    public void Load(SaveData data)
    {
        if (data.id == GetID())
        {
            water.transform.position = data.position;
            isNotUsed = data.customFloat > 0.5f;
        }
    }

}
