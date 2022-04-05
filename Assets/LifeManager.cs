using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public bool isDead = false;
    public GameObject gameOverPanel;

    public bool godMode = false;

    public static LifeManager instance;
    public GameObject destroyAnimation;
    public MeshRenderer playerMesh; 

    private void Awake()
    {
        instance = this;
    }

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
            if (!isDead)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        isDead = true;
        LevelManager.instance.StartInterval();
        Instantiate(destroyAnimation, transform.position, Quaternion.identity, transform);
        playerMesh.enabled = false;
    }
}
