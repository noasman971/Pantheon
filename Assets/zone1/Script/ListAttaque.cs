using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ListAttaque : MonoBehaviour
{
    public GameObject[] AllAttack;
    public List<string> attack = new List<string>();
    private string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/attack.json";
        LoadList(); // Charger la liste au lancement
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (string kataras in attack)
            {
                Debug.Log(kataras);
            }
        }
  
        
    }

    public void AddAttack(string attackName)
    {
        attack.Add(attackName);
        SaveList();
    }

    private void SaveList()
    {
        string json = JsonUtility.ToJson(new SaveData(attack));
        File.WriteAllText(filePath, json);
    }

    private void LoadList()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            attack = data.attack;
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public List<string> attack;
        public SaveData(List<string> list) { attack = list; }
    }
}
