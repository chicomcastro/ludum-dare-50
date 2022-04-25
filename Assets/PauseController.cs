using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused = false;

    public static PauseController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            LogManager.instance.Log("PRESS_PAUSE");
            bool newPauseStatus = !pauseMenu.activeInHierarchy;
            pauseMenu.SetActive(newPauseStatus);
            isPaused = newPauseStatus;
        }
    }
}
