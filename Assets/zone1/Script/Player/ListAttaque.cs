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
        LoadList(); 
    }
    

    /// <summary>
    /// Adds a new attack name to the list and saves the list.
    /// </summary>
    /// <param name="attackName">The name of the attack to be added.</param>
    public void AddAttack(string attackName)
    {
        attack.Add(attackName);
        SaveList();
    }

    /// <summary>
    /// Saves the attack list to a JSON file.
    /// </summary>
    private void SaveList()
    {
        string json = JsonUtility.ToJson(new SaveData(attack));
        File.WriteAllText(filePath, json);
        Debug.Log("File saved : " + filePath);

    }
    
    /// <summary>
    /// Clears the attack list in memory and empties the saved file.
    /// </summary>
    public void ClearList()
    {
        attack.Clear(); // Vide la liste en mémoire
        File.WriteAllText(filePath, ""); // Écrit un fichier vide
        Debug.Log("Fichier JSON vidé : " + filePath);
    }

    /// <summary>
    /// Loads the attack list from a JSON file.
    /// </summary>
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
        
        /// <summary>
        /// Constructor to initialize the SaveData with the attack list.
        /// </summary>
        /// <param name="list">The list of attacks to be saved.</param>
        public SaveData(List<string> list)
        {
            attack = list;
        }
    }
}
