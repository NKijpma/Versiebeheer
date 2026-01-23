using UnityEngine;

public class SpiderEnemy_ai : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 spawnPos;
    private float maxUp;
    private float maxDown;
    private int direction = -1;
    private readonly float moveSpeed = 2f;

    private void Start()
    {
        spawnPos = transform.position;
        maxUp = spawnPos.y; //+ 4f;
        maxDown = spawnPos.y - 4f;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, direction * moveSpeed);

        if (transform.position.y >= maxUp)
        {
            direction = -1;
        }
        else if (transform.position.y <= maxDown)
        {
            direction = 1;
        }
    }

}
