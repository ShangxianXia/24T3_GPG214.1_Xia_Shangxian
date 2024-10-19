using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JSONSave : MonoBehaviour
{
    public DataToBeSaved dataToBeSavedRef = new DataToBeSaved();

    public string jsonFile = "JSONFile.json";

    public string folderPath = Application.streamingAssetsPath;

    private string fullFilePath = string.Empty;

    public GameObject playerToTeleport;

    void Start()
    {
        // makes the file path
        fullFilePath = Path.Combine(folderPath, jsonFile);
        // finds Ellen
        playerToTeleport = GameObject.Find("Ellen");
        // saves ellens name in the JSon file
        dataToBeSavedRef.nameToBeSaved = GameObject.Find("Ellen").name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            // increments the variable
            dataToBeSavedRef.timesThePlayerSaved++;
            Save();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // increments the variable
            dataToBeSavedRef.timesThePlayerLoaded++;
            Load();
        }
    }
     
    public void Save()
    {
        // sets the players to the saved position
        dataToBeSavedRef.SetPlayerPosition(playerToTeleport.transform.position);

        Debug.Log("Name thats saved: " + dataToBeSavedRef.nameToBeSaved);
        // converts the public variables in dataToBeSavedRef to become JSONfile writable
        string jsonData = JsonUtility.ToJson(dataToBeSavedRef);
        // writes the public variables in dataToBeSavedRef
        File.WriteAllText(fullFilePath, jsonData);

        Debug.Log("Player saved to json file: " + dataToBeSavedRef.timesThePlayerSaved + " times!");
    }

    public void Load()
    {
        if (File.Exists(fullFilePath))
        {
            // reads all the JSON file data
            string jsonData = File.ReadAllText(fullFilePath);

            // converts json file stuff to the dataToBeSavedRef
            dataToBeSavedRef = JsonUtility.FromJson<DataToBeSaved>(jsonData);

            if (dataToBeSavedRef != null)
            {
                // teleports the player to the recently saved position
                Debug.Log("Player loaded json file: " + dataToBeSavedRef.timesThePlayerLoaded + " times!");
                playerToTeleport.transform.position = dataToBeSavedRef.ReturnPlayerPosition();
                Debug.Log("Name thats loaded: " + dataToBeSavedRef.nameToBeSaved);
            }
            else
            {
                Debug.Log("Json is not put into file");
            }
        }
        else
        {
            Debug.Log("No player position data file");
        }
    }

}
