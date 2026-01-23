using System.Collections;
using UnityEngine;

public class GenericWandering_ai : MonoBehaviour
{
    // rb voor script
    [Header("RigidBody2D")]
    public Rigidbody2D rb;
    // richting van bewegen en snelheid
    private float direction = 1f;

    // start freeze y na spawn
    private void Start()
    {
        StartCoroutine(Freeze_Y_timer());
    }

    // beweeg enemy constant in een richting
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * 2f, rb.linearVelocity.y);
    }
    // draait enemy om
    void Flip()
    {
        direction *= -1f;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // als hitbox de grond verlaat, draai om
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Flip();
        }
    }

    // als hitbox een muur draai om
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Flip();
        }
    }

    // enemy spawnt in en dan word y bevroren
    void Freeze_Y()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator Freeze_Y_timer()
    {
        yield return new WaitForSeconds(2f);
        Freeze_Y();

    }

}
