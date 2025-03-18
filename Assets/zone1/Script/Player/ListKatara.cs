using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ListKatara : MonoBehaviour
{
    public List<string> katara = new List<string>();
    private string filePath;

    
    /// <summary>
    /// Initializes the file path for the JSON file and loads the list when the script starts.
    /// </summary>
    private void Start()
    {
        filePath = Application.persistentDataPath + "/katara.json";
        LoadList(); // Charger la liste au lancement
    }


    /// <summary>
    /// Clears the katara list in memory and updates the saved file with an empty katara list.
    /// </summary>
    public void ClearList()
    {
        katara.Clear(); // Vide la liste en mémoire
        string json = JsonUtility.ToJson(new SaveData(katara)); // Convertit la liste vide en JSON
        File.WriteAllText(filePath, json); // Écrit dans le fichier JSON
        Debug.Log("Liste vidée et fichier mis à jour : " + filePath);
    }

    
    
    /// <summary>
    /// Adds a new katara to the list and saves the updated list to the file.
    /// </summary>
    /// <param name="kataraName">The name of the katara to be added to the list.</param>
    public void AddKatara(string kataraName)
    {
        katara.Add(kataraName);
        SaveList();
    }

    
    /// <summary>
    /// Saves the katara list to a JSON file.
    /// </summary>
    private void SaveList()
    {
        string json = JsonUtility.ToJson(new SaveData(katara));
        File.WriteAllText(filePath, json);
    }

    
    /// <summary>
    /// Loads the katara list from a JSON file if it exists.
    /// </summary>
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

        /// <summary>
        /// Constructor to initialize the SaveData class with a katara list.
        /// </summary>
        /// <param name="list">The katara list to be saved.</param>
        public SaveData(List<string> list)
        {
            katara = list;
        }
    }
}