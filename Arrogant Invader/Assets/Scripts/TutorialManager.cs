using System.Collections;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject timeline;
    public GameObject[] popUps;

    public GameManager gameManager;
    public TutorialEnemy tutorialEnemy;
    private Player playerInstance;
    private AudioManager audioManager;

    public float firstPopUpWaitTime = 3f;
    public float secondPopUpWaitTime = 5f;
    private int popUpIndex = 0;

    
    
    void Awake()
    {
        playerInstance = FindObjectOfType<Player>();
        audioManager = FindObjectOfType<AudioManager>();
        playerInstance.enabled = false;
        playerInstance.cooldownTimer = 1500f;
        timeline.SetActive(false);
        
    }

    private void Start()
    {
        audioManager.Play("PopUp1");
    }

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (popUpIndex == i)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                playerInstance.enabled = true;
                popUpIndex++;
                audioManager.Stop("PopUp1");
                audioManager.Play("PopUp2");

            } 
        }
        else if (popUpIndex == 1)
        {
            
            firstPopUpWaitTime -= Time.deltaTime;
            timeline.SetActive(true);
            if (firstPopUpWaitTime < 0)
            {
                
                playerInstance.cooldownTimer = 0f;
                popUpIndex++;
                audioManager.Stop("PopUp2");
                audioManager.Play("PopUp3");

                //waitTime = 3f;
            }
            
        }
        else if (popUpIndex == 2)
        {
            
            if (tutorialEnemy.enemyIsKilled)
            {
                popUpIndex++;
                audioManager.Stop("PopUp3");
                audioManager.Play("PopUp4");
            }
        }
        else if (popUpIndex == 3)
        {
            secondPopUpWaitTime -= Time.deltaTime;
            if (secondPopUpWaitTime < 0)
            {
                FindObjectOfType<AudioManager>().Play("Siren");
                audioManager.Play("PopUp5");
                popUpIndex++;
                secondPopUpWaitTime = 7f;
            }
        }
        else if (popUpIndex == 4)
        {
            secondPopUpWaitTime -= Time.deltaTime;
            if (secondPopUpWaitTime < 0)
            {
                FindObjectOfType<SceneLoader>().LoadNextLevel();
            }
        }
    }

    
}
