using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    private SpriteRenderer sr;
    public Rigidbody2D rb;
    [HideInInspector] public Vector2 spawnPos;
    //-------------------------------------------------------------
    [Header("Jumping Mechanics")]
    public bool onWall = false;
    public bool onGround = false;
    private float jumpTimer = 0f;
    //-------------------------------------------------------------
    [Header("Player Stats")]
    public float Player_MoveSpeed = 4f;
    public float Player_JumpHeight = 4f;
    public float JumpCooldown = 0.8f;
    //-------------------------------------------------------------
    [Header("Keybinds Left_Movement")]
    private readonly KeyCode left1 = KeyCode.A;
    private readonly KeyCode left2 = KeyCode.W;
    private readonly KeyCode left3 = KeyCode.LeftArrow;
    private readonly KeyCode left4 = KeyCode.UpArrow;
    [Header("Keybinds Right_Movement")]
    private readonly KeyCode right1 = KeyCode.D;
    private readonly KeyCode right2 = KeyCode.S;
    private readonly KeyCode right3 = KeyCode.RightArrow;
    private readonly KeyCode right4 = KeyCode.DownArrow;
    [Header("List van alle keys")]
    private readonly List<KeyCode> keys = new List<KeyCode>();
    [Header("misc")]
    private readonly KeyCode QuickRespawn = KeyCode.R;
    //-------------------------------------------------------------

    [System.Serializable]
    public class HealthStats
    {
        public int maxHealth = 40;
        public int currentHealth;
        public Heal healtype;
    }

    private void Start()
    {
        spawnPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        // verbeterde a en d beweging
        float x = 0f;

        // check laatste ingedrukt in keys list
        for (int i = keys.Count - 1; i >= 0; i--)
        {
            KeyCode k = keys[i];
            if (k == left1 || k == left2 || k == left3 || k == left4) { x = -1f; break; }
            if (k == right1 || k == right2 || k == right3 || k == right4) { x = 1f; break; }
        }

        // zorgt voor movement
        rb.linearVelocity = new Vector2(x * Player_MoveSpeed, rb.linearVelocity.y);

        // respawn als onder -10 y gaat
        if (transform.position.y <= -10)
            Respawn();

        // snelle respawn met R
        if (Input.GetKeyDown(QuickRespawn))
            Respawn();

        // checkt per frame of een key is ingedrukt of losgelaten
        KeyCode[] keysToCheck = { left1, left2, left3, left4, right1, right2, right3, right4 };

        foreach (KeyCode key in keysToCheck)
        {
            if (Input.GetKeyDown(key)) Add(key);
            if (Input.GetKeyUp(key)) Remove(key);
        }

        // springen
        jumpTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && onGround && jumpTimer <= 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Player_JumpHeight);
            jumpTimer = JumpCooldown;
        }

        // flip sprite
        if (x < 0) sr.flipX = true;
        if (x > 0) sr.flipX = false;
    }


    // checken of speler op de grond is
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f) // alleen als speler van boven komt
                {
                    onGround = true;

                }
                if (contact.normal.y > 0.5f && Mathf.Abs(contact.normal.x) < 0.5f)
                {
                    onGround = true;
                }
            }
        }
    }

    //niet meer op de grond
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onWall = false;
            onGround = false;
        }
    }

    //voegt key toe aan een lijst  en dan word het de laatste ingedrukte key
    void Add(KeyCode k) { if (keys.Contains(k)) keys.Remove(k); keys.Add(k); }
    void Remove(KeyCode k) { keys.Remove(k); }

    // functie voor respawn
    public void Respawn()
    {
        transform.position = spawnPos + Vector2.up * 0.5f; // iets boven spawn
        rb.linearVelocity = Vector2.zero;
        onGround = false;
    }

    // functie om spawnpos te updaten
    public void SetSpawn(Vector2 newSpawn)
    {
        spawnPos = newSpawn;
    }
}
