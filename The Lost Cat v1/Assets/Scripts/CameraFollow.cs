using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance;

    [SerializeField] private Transform player;
    [SerializeField, Range(2f, 5f)] private float speed;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        var x = Mathf.Lerp(transform.position.x, player.position.x + offset.x, Time.deltaTime * speed);
        var y = Mathf.Lerp(transform.position.y, player.position.y + offset.y, Time.deltaTime * speed);
        transform.position = new Vector3(x, y, transform.position.z);
    }
}
