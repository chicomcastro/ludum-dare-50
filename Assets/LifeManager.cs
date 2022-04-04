using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public bool isDead = false;
    public GameObject gameOverPanel;

    public bool godMode = false;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            if (godMode)
            {
                return;
            }
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        isDead = true;
    }
}
