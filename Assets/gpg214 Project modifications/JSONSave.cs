using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSave : MonoBehaviour
{
    public DataToBeSaved dataToBeSavedRef = new DataToBeSaved();

    public string jsonFile = "JSONFile.json";

    public string folderPath = Application.streamingAssetsPath;

    private string fullFilePath = string.Empty;

    public GameObject playerToTeleport;

    //public int timesThePlayerSaved;

    //public int timesThePlayerLoaded;

    // Start is called before the first frame update
    void Start()
    {
        fullFilePath = Path.Combine(folderPath, jsonFile);

        playerToTeleport = GameObject.Find("Ellen");

        dataToBeSavedRef.nameToBeSaved = GameObject.Find("Ellen").name;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            dataToBeSavedRef.timesThePlayerSaved++;
            Save();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            dataToBeSavedRef.timesThePlayerLoaded++;
            Load();
        }
    }
     
    public void Save()
    {
        dataToBeSavedRef.SetPlayerPosition(playerToTeleport.transform.position);

        Debug.Log("Name thats saved: " + dataToBeSavedRef.nameToBeSaved);

        string jsonData = JsonUtility.ToJson(dataToBeSavedRef);

        File.WriteAllText(fullFilePath, jsonData);

        Debug.Log("Player saved to json file: " + dataToBeSavedRef.timesThePlayerSaved + " times!");
    }

    public void Load()
    {
        if (File.Exists(fullFilePath))
        {
            string jsonData = File.ReadAllText(fullFilePath);

            dataToBeSavedRef = JsonUtility.FromJson<DataToBeSaved>(jsonData);

            if (dataToBeSavedRef != null)
            {
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
