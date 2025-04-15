using System;
using UnityEngine;

public class WellAction : MonoBehaviour
{
    private bool isNear = false;
    private float speed = 0;
    [SerializeField]private GameObject water;
    private Vector2 finalPos;
    [SerializeField] private GameObject invisibleWall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finalPos = new Vector2 (water.transform.position.x,water.transform.position.y+1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E)) && isNear)
        {
            speed = 1;
        }
        else
        {
            speed = 0;
        }

        
        var y = Mathf.Lerp(water.transform.position.y, finalPos.y, Time.deltaTime * speed);
        water.transform.position = new Vector2(finalPos.x, y);
        if (Math.Abs(water.transform.position.y - finalPos.y) < 0.01f) 
        {
            Destroy(invisibleWall);
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
}
