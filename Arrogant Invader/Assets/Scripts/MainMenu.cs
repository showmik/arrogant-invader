using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void StartGame()
    {
        FindObjectOfType<SceneLoader>().LoadNextLevel();
    }
}
