using System;
using System.Collections.Generic;
using UnityEngine;

internal class LanguageHandler : MonoBehaviour
{
    public static Dictionary<string, string> CachedWords = new Dictionary<string, string>();

    public static string CurrentLanguage = "EN";

    public void Start()
    {
        SwitchLanguage("FR");
    }

    public static void SwitchLanguage(string newLanguage)
    {
        CachedWords.Clear();
        CurrentLanguage = newLanguage;
        CacheWords();
    }

    public static void CacheWords()
    {
        TextAsset textFile = Resources.Load<TextAsset>("Lang/" + CurrentLanguage);

        if (textFile == null)
        {
            Debug.LogError("Language Text File does not exist");
            return;

        }
        string[] lines = textFile.text.Split(';');

        foreach (string line in lines)
        {
            if (line == null)
                continue;

            string[] split = line.Split('=');

            if (CachedWords.ContainsKey(split[0]))
                continue;

            CachedWords.Add(split[0], split[1].Replace("\"", ""));
        }
    }

    internal static string GetLocalizedName(string unlocalizedName)
    {
        Debug.Log(CachedWords.Count);

        if (CachedWords.ContainsKey(unlocalizedName))
            return CachedWords[unlocalizedName];

        return unlocalizedName;
    }
}