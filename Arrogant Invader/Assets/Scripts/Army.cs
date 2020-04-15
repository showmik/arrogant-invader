using UnityEngine;

public class Army : MonoBehaviour
{
    public float marchTime = 1f;
    public float moveDistance = 0.5f;
    public float maxAnimSpeed = 4;
    public int enemyCount = 0;

    private int direction = 1;
    private float timeCounter;
    private bool canMoveHorizontal = true;
    private bool movedDown = false;
    private float decreaseAmount;
    private float animIncreaseRate;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        timeCounter = marchTime;
        direction = 1;
        decreaseAmount = (marchTime - 0.04f) / (float)(transform.childCount - 1);
        animIncreaseRate = maxAnimSpeed / (transform.childCount - 1);

        foreach (Transform alien in transform)
        {
            enemyCount++;
        }
    }

    void Update()
    {
        timeCounter -= Time.deltaTime;

        if (timeCounter < 0)
        {
            CheckMarchSituation();
            March();
            timeCounter = marchTime;
        }

    }

    public void MoveHorizontal()
    {
        transform.position += new Vector3(moveDistance * direction, 0, 0);
        movedDown = false;
    }

    public void MoveDown()
    {
        transform.position += new Vector3(0, -0.5f, 0);
        direction *= -1;
        canMoveHorizontal = true;
        movedDown = true;
    }

    public void IncreaseMarchSpeed()
    {
        marchTime -= decreaseAmount;
    }

    public void IncreaseAnimSpeed()
    {
        foreach (Animator animator in gameObject.GetComponentsInChildren<Animator>())
        {
            animator.speed += animIncreaseRate;
        }
    }

    public void CheckMarchSituation()
    {
        foreach (Transform alien in transform)
        {
            float currentY = alien.transform.position.y;

            if (currentY <= 0.5f)
            {
                
                GameManager.gameOver = true;
                gameManager.GameOver();
            }
            
            if ((alien.transform.position.x >= 2.5f || alien.transform.position.x <= -2.5f) && !movedDown)
            {
                canMoveHorizontal = false;
                break;
            }
        }

        if (enemyCount <= 0)
        {
            Destroy(gameObject);
            gameManager.GameWon();
        }

    }

    public void March()
    {
        if (canMoveHorizontal)
        {
            MoveHorizontal();
        }
        else if (!canMoveHorizontal)
        {
            MoveDown();
        }
    }


}
