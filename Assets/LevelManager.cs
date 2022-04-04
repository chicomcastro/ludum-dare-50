using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;

    public static LevelManager instance;
    public PlayerMovement playerMovement;

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
    }

    private IEnumerator Level0()
    {
        yield return new WaitUntil(() => currentLevel == 1);
    }

    private IEnumerator Level1()
    {
        yield return new WaitForSeconds(10f);
        StartInterval();
    }

    private IEnumerator Level2()
    {
        playerMovement.EnableDelay(KeyLabel.right);
        yield return new WaitForSeconds(30f);
        StartInterval();
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
