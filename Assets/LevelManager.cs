using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Text countDownText;

    private bool isInInterval = true;

    public Text levelText;

    public GameObject fallingBoxes;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentLevel = 0;
        StartCoroutine("Level0");
        delayStatusPanel.SetActive(false);
        fallingBoxes.SetActive(false);
    }

    private IEnumerator Level0()
    {
        LogManager.instance.Log("START_LEVEL_0");
        yield return new WaitUntil(() => currentLevel == 1);
    }

    private IEnumerator Level1()
    {
        LogManager.instance.Log("START_LEVEL_1");
        yield return new WaitForSeconds(levelDuration);
        StartInterval();
    }

    private IEnumerator Level2()
    {
        LogManager.instance.Log("START_LEVEL_2");
        delayStatusPanel.SetActive(true);
        playerMovement.EnableDelay(KeyLabel.right);
        yield return new WaitForSeconds(levelDuration);
        StartInterval();
    }

    private IEnumerator Level3()
    {
        LogManager.instance.Log("START_LEVEL_3");
        playerMovement.EnableDelay(KeyLabel.up);
        yield return new WaitForSeconds(levelDuration);
        StartInterval();
    }

    private IEnumerator Level4()
    {
        LogManager.instance.Log("START_LEVEL_4");
        playerMovement.EnableDelay(KeyLabel.left);
        playerMovement.EnableDelay(KeyLabel.down);
        yield return new WaitForSeconds(levelDuration + 1.5f);
        countDownText.text = "Time left: until you die";
        StartCoroutine("FallingBoxesRoutine");
    }

    private IEnumerator FallingBoxesRoutine()
    {
        LogManager.instance.Log("START_FALLING_BOXES");
        Rigidbody[] boxes = fallingBoxes.GetComponentsInChildren<Rigidbody>();
        boxes.ToList().ForEach((rigidbody) =>
        {
            rigidbody.useGravity = false;
        });
        fallingBoxes.SetActive(true);
        for (int i = 0; i < boxes.Length; i++)
        {
            Rigidbody rigidbody = boxes[i];
            rigidbody.useGravity = true;
            yield return new WaitForSeconds(1f);
        }
        LogManager.instance.Log("END_FALLING_BOXES");
    }

    private IEnumerator CountDown()
    {
        for (int i = 0; i <= levelDuration; i++)
        {
            countDownText.text = "Time left: " + (levelDuration - i);
            yield return new WaitForSeconds(1f);
        }
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
            StartCoroutine("CountDown");
        }
    }

    public bool IsInInterval()
    {
        return isInInterval;
    }
}
