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
        for (int i = 0; i < dialogLength; i++)
        {
            currentShowingText.text += dialogs[currentDialogIndex][i];
            yield return new WaitForSeconds(1f/textSpeed);
        }
        finishCurrentDialog = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (finishCurrentDialog)
            {
                finishCurrentDialog = false;
                currentDialogIndex = Mathf.Min(currentDialogIndex + 1, dialogs.Length - 1);

                int currentLevel = GetCurrentLevel();
                if (currentDialogIndex > levelDialogs[currentLevel].endIndex)
                {
                    dialogPanel.SetActive(false);
                }
                else
                {
                    StartCoroutine("ShowCurrentDialog");
                }
                return;
            }
            print("Skipping dialog");
            StopCoroutine("ShowCurrentDialog");
            currentShowingText.text = dialogs[currentDialogIndex];
            finishCurrentDialog = true;
        }
    }

    private int GetCurrentLevel()
    {
        return 0;
    }
}

[System.Serializable]
public class LevelDialogs
{
    public int startIndex;
    public int endIndex;
}
