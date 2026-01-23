using System.Collections.Generic;
using UnityEngine;

public class NewSpawnPoint : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.spawnPos = transform.position + new Vector3(0, 5);
            }
        }
    }
}

