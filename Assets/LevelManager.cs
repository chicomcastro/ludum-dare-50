using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    public int maxLevel;
    public float levelDuration = 10f;

    public static LevelManager instance;
    public PlayerMovement playerMovement;
    public GameObject delayStatusPanel;

    private bool isInInterval = true;

    public Text levelText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentLevel = 0;
        StartCoroutine("Level0");
        delayStatusPanel.SetActive(false);
    }

    private IEnumerator Level0()
    {
        yield return new WaitUntil(() => currentLevel == 1);
    }

    private IEnumerator Level1()
    {
        yield return new WaitForSeconds(levelDuration);
        StartInterval();
    }

    private IEnumerator Level2()
    {
        delayStatusPanel.SetActive(true);
        playerMovement.EnableDelay(KeyLabel.right);
        yield return new WaitForSeconds(levelDuration);
        StartInterval();
    }

    private IEnumerator Level3()
    {
        playerMovement.EnableDelay(KeyLabel.up);
        yield return new WaitForSeconds(levelDuration);
        StartInterval();
    }

    private IEnumerator Level4()
    {
        playerMovement.EnableDelay(KeyLabel.left);
        playerMovement.EnableDelay(KeyLabel.down);
        yield return new WaitForSeconds(levelDuration);
    }

    public void StartInterval()
    {
        print("Start new interval");
        isInInterval = true;
    }

    public void FinishInterval(bool startCoroutine = false)
    {
        print("Finish interval");
        isInInterval = false;
        NextLevel(startCoroutine);
    }

    public void NextLevel(bool startCoroutine=false)
    {
        print("Going to next level");
        currentLevel++;
        levelText.text = "Level: " + currentLevel.ToString();
        if (startCoroutine)
        {
            StartCoroutine("Level" + (currentLevel).ToString());
        }
    }

    public bool IsInInterval()
    {
        return isInInterval;
    }
}
