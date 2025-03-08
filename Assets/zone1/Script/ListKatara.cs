using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ListKatara : MonoBehaviour
{
    public List<string> katara = new List<string>();
    private string filePath;

    private void Start()
    {
        filePath = Application.persistentDataPath + "/katara.json";
        LoadList(); // Charger la liste au lancement
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (string kataras in katara)
            {
                Debug.Log(kataras);
            }
        }

    }

    public void AddKatara(string kataraName)
    {
        katara.Add(kataraName);
        SaveList();
    }

    private void SaveList()
    {
        string json = JsonUtility.ToJson(new SaveData(katara));
        File.WriteAllText(filePath, json);
    }

    private void LoadList()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            katara = data.katara;
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public List<string> katara;
        public SaveData(List<string> list) { katara = list; }
    }
}