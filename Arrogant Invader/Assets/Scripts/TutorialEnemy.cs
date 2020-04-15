using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    public bool enemyIsKilled = false;
    public GameObject explosionEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMissile"))
        {
            enemyIsKilled = true;
            FindObjectOfType<AudioManager>().Play("Hurt");
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameObject explsion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(explsion, 0.1f);
        }
    }
}
