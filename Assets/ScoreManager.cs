using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text[] scoreTexts;
    public float score;

    public float multiplyFactor = 500f;

    public PlayerMovement player;

    void Start()
    {
        score = 0f;    
    }

    void Update()
    {
        if (PauseController.instance.isPaused)
        {
            return;
        }

        scoreTexts.ToList().ForEach(scoreText =>
        {
            scoreText.text = "Score: " + ((int)score).ToString();
        });

        if (!LevelManager.instance.IsInInterval())
        {
            score += (Time.deltaTime * multiplyFactor * player.rb.velocity.magnitude);
        }
    }
}
