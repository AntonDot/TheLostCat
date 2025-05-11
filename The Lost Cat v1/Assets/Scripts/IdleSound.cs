using UnityEngine;

public class IdleRandomSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] idleClips;
    [SerializeField] private float idleInterval = 5f;
    [SerializeField] private float movementThreshold = 0.01f;
    [SerializeField] private AudioSource audioSource;

    private Vector3 lastPosition;
    private float timer;

    void Start()
    {
        lastPosition = transform.position;
        timer = idleInterval;
    }

    void Update()
    {
        bool isIdle = Vector3.Distance(transform.position, lastPosition) < movementThreshold;

        if (isIdle)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f && idleClips.Length > 0 && !audioSource.isPlaying)
            {
                int index = Random.Range(0, idleClips.Length);
                audioSource.clip = idleClips[index];
                audioSource.Play();

                timer = idleInterval;
            }
        }
        else
        {
            timer = idleInterval;
        }

        lastPosition = transform.position;
    }
}
