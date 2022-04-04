using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    [TextArea]
    public string[] dialogs;

    public Text currentShowingText;
    public float textSpeed = 20f;

    private int currentDialog = 0;
    private bool finishCurrentDialog = false;

    private void Start()
    {
        currentDialog = 0;
        StartCoroutine("ShowCurrentDialog");
    }

    private IEnumerator ShowCurrentDialog()
    {
        int dialogLength = dialogs[currentDialog].Length;
        currentShowingText.text = "";
        for (int i = 0; i < dialogLength; i++)
        {
            currentShowingText.text += dialogs[currentDialog][i];
            yield return new WaitForSeconds(1f/textSpeed);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (finishCurrentDialog)
            {
                finishCurrentDialog = false;
                currentDialog = Mathf.Min(currentDialog + 1, dialogs.Length - 1);
                StartCoroutine("ShowCurrentDialog");
                return;
            }
            print("Skipping dialog");
            StopCoroutine("ShowCurrentDialog");
            currentShowingText.text = dialogs[currentDialog];
            finishCurrentDialog = true;
        }
    }
}
