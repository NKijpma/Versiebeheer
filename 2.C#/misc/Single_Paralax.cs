using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect; // 0 = beweegt mee met camera, 1 = blijft stil, 0.5 = halve snelheid

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // herhaal de achtergrond
        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
    }
}