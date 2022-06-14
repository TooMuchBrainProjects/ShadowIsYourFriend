using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Highscores : MonoBehaviour
{
    [Header("Classes")]
    DisplayHighscores highscoresDisplay;
    static Highscores instance;

    [Header("Website Settings")]
    const string privateCode = "KcXP-PVnJ0m3b7B9UvqdOAm2eQIpMLukmONJnjzG6Mhw";
    const string publicCode = "62a89fce8f40bb11c081597e";
    const string webURL = "http://dreamlo.com/lb/";

    [Header("Highscore Variables")]
    [HideInInspector] public Highscore[] highscoresList;

    void Awake()
    {
        instance = this;
        highscoresDisplay = GetComponent<DisplayHighscores>();

        Highscores.AddNewHighscore(PlayerPrefs.GetString("Username"), PlayerPrefs.GetInt("Highscore"));
    }

    public static void AddNewHighscore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    //IEnumerator UploadNewHighscore(string username, int score)
    //{
    //    UnityWebRequest unityWebRequest = new UnityWebRequest(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
    //    yield return unityWebRequest;

    //    if (string.IsNullOrEmpty(unityWebRequest.error))
    //        print("Upload Successful");
    //    else
    //        print("Error uploading: " + unityWebRequest.error);
    //}

    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Upload Successful");
            DownloadHighscores();
        }
        else
            print("Error uploading: " + www.error);
    }

    public void DownloadHighscores()
    {
        StartCoroutine(DownloadHighscoresFromDatabase());
    }

    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighscores(www.text);
            highscoresDisplay.OnHighscoresDownloaded(highscoresList);
        }
        else
            print("Error downloading: " + www.error);
    }

    void FormatHighscores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);

            highscoresList[i] = new Highscore(username, score);
            print(highscoresList[i].username + ": " + highscoresList[i].score);
        }
    }
    public static void DeleteHighscore(string username)
    {
        instance.StartCoroutine(instance.DeleteHighscoreFromDatabase(username));
    }

    IEnumerator DeleteHighscoreFromDatabase(string username)
    {
        WWW www = new WWW(webURL + privateCode + "/delete/" + WWW.EscapeURL(username));
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            print("Deleted Successful");
            DownloadHighscores();
        }
        else
            print("Error deleting: " + www.error);
    }
}

public struct Highscore
{
    public string username;
    public int score;

    public Highscore(string _username, int _score)
    {
        username = _username;
        score = _score;
    }
}