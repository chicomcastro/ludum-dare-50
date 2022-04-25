using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LogManager : MonoBehaviour
{
    private string uniqueId;

    public bool enableLogging = true;

    public static LogManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        uniqueId = System.Guid.NewGuid().ToString();
        print(uniqueId);
        // TODO save in player prefs and check if already has

        Log("NEW_SECTION");
    }

    public void Log(string eventName, object eventData = null)
    {
        if (enableLogging)
        {
            print(eventName);
            print(JsonUtility.ToJson(eventData));
            StartCoroutine(SendLog(eventName, eventData));
        }
    }

    IEnumerator SendLog(string eventName, object eventData)
    {
        WWWForm formData = new WWWForm();
        formData.AddField("application", "sabotage");
        formData.AddField("event", eventName);
        formData.AddField("anonymousId", uniqueId);
        if (eventData != null)
        {
            formData.AddField("json_data", JsonUtility.ToJson(eventData));
        }
        formData.AddField("version", "1.1.0");

        UnityWebRequest www = UnityWebRequest.Post("https://game-logging.herokuapp.com/v1/tracks", formData);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }
}
