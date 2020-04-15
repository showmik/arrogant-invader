using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Control")]
    public float boundary = 2f;
    public float moveSpeed = 3f;

    [Header("Firing Options")]
    public GameObject missilePrefab;
    public float missileSpeed = 4f;
    public float fireCooldownTime = 1f;
    public float cooldownTimer;

    private Animator animator;
    private AudioManager audioManager;
    private GameManager gameManager;
    private Rigidbody2D body2d;

    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        
    }

    void Update()
    {
        float axisValue = Input.GetAxisRaw("Horizontal");
        body2d.velocity = new Vector2(axisValue * moveSpeed, 0f);

        if (transform.position.x > boundary)
        {
            transform.position = new Vector2(boundary, transform.position.y);
            body2d.velocity = Vector2.zero;
            
        }
        else if (transform.position.x < -boundary)
        {
            transform.position = new Vector2(-boundary, transform.position.y);
            body2d.velocity = Vector2.zero;
        }

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0 && Input.GetMouseButtonDown(0))
        {
            Fire();
            cooldownTimer = fireCooldownTime;
        }
    }

    private void Fire()
    {
        audioManager.Play("Shoot");
        GameObject missileInstance = Instantiate(missilePrefab);
        missileInstance.transform.position = transform.position + new Vector3(0, .3f, 0);
        missileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, missileSpeed);
        Destroy(missileInstance, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyMissile"))
        {
            if (gameManager.playerLives <= 0)
            {
                gameManager.GameOver();
            }
            else
            {
                Destroy(collision.gameObject);
                StartCoroutine(Die());
                
            }
        }
    }

    public IEnumerator Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        gameManager.playerLives--;
        gameManager.healthUI[GameManager.healthIndex].SetActive(false);
        audioManager.Play("Explode");
        animator.SetTrigger("StartDeathAnim");

        if (GameManager.healthIndex > 0)
            GameManager.healthIndex--;

        moveSpeed = 0;
        cooldownTimer = fireCooldownTime;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        gameManager.SpawnPlayer();
        
    }
}
