using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [TextArea]
    public string[] dialogs;

    public Text currentShowingText;
    public float textSpeed = 20f;
    public GameObject dialogPanel;

    public LevelDialogs[] levelDialogs;

    private int currentDialogIndex = 0;
    private bool finishCurrentDialog = false;

    private void Start()
    {
        currentDialogIndex = 0;
        StartCoroutine("ShowCurrentDialog");
    }

    private IEnumerator ShowCurrentDialog()
    {
        int dialogLength = dialogs[currentDialogIndex].Length;
        currentShowingText.text = "";
        print(GetCurrentLevel());
        print(currentDialogIndex);
        for (int i = 0; i < dialogLength; i++)
        {
            currentShowingText.text += dialogs[currentDialogIndex][i];
            yield return new WaitForSeconds(1f/textSpeed);
        }
        finishCurrentDialog = true;
    }

    private void Update()
    {
        if (!LevelManager.instance.IsInInterval())
        {
            return;
        }

        int currentLevel = GetCurrentLevel();
        if (currentLevel >= GetMaxLevel())
        {
            return;
        }

        if (!dialogPanel.activeInHierarchy)
        {
            dialogPanel.SetActive(true);
            StartCoroutine("ShowCurrentDialog");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (finishCurrentDialog)
            {
                finishCurrentDialog = false;
                currentDialogIndex = Mathf.Min(currentDialogIndex + 1, dialogs.Length - 1);

                if (currentDialogIndex > levelDialogs[currentLevel].endIndex)
                {
                    currentDialogIndex++;
                    dialogPanel.SetActive(false);
                    LevelManager.instance.FinishInterval(true);
                }
                else
                {
                    StartCoroutine("ShowCurrentDialog");
                }
                return;
            }
            StopCoroutine("ShowCurrentDialog");
            currentShowingText.text = dialogs[currentDialogIndex];
            finishCurrentDialog = true;
        }
    }

    private int GetCurrentLevel()
    {
        return LevelManager.instance.currentLevel;
    }

    private int GetMaxLevel()
    {
        return LevelManager.instance.maxLevel;
    }
}

[System.Serializable]
public class LevelDialogs
{
    public int startIndex;
    public int endIndex;
}
