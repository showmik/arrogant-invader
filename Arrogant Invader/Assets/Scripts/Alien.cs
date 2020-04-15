using UnityEngine;

public class Alien : MonoBehaviour
{
    public GameObject missilePrefab;
    public GameObject explosionEffectPrefab;

    public float minFireTime = 6f;
    public float maxFireTime = 15f;
    public float enemyMissileSpeed;

    private Army army;
    private AudioManager audioManager;
    private float fireTime;
    private float fireTimeCounter;


    private void Start()
    {
        fireTime = UnityEngine.Random.Range(minFireTime, maxFireTime);
        fireTimeCounter = fireTime;
        army = gameObject.GetComponentInParent<Army>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        fireTimeCounter -= Time.deltaTime;

        if (fireTimeCounter < 0)
        {
            Fire();
            fireTimeCounter = fireTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerMissile"))
        {
            Destroy(collision.gameObject);
            Explode();
            MakeArmyFaster();
        }
    }

    private void Fire()
    {
        audioManager.Play("EnemyShoot");
        GameObject missileInstance = Instantiate(missilePrefab);
        missileInstance.transform.position = transform.position + new Vector3(0, -0.3f, 0);
        missileInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyMissileSpeed);
        Destroy(missileInstance, 5f);
    }

    public void Explode()
    {
        audioManager.Play("Hurt");
        Destroy(gameObject);
        army.enemyCount--;
        GameObject explsion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        Destroy(explsion, 0.1f);
    }

    public void MakeArmyFaster()
    {
        army.IncreaseMarchSpeed();
        army.IncreaseAnimSpeed();
    }
}
