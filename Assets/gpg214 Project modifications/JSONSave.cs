using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONSave : MonoBehaviour
{
    public DataToBeSaved dataToBeSavedRef = new DataToBeSaved();
    public string playerVector3Cords = "Players position.json";
    public string folderPath = Application.streamingAssetsPath;
    private string fullFilePath = string.Empty;
    public GameObject playerToTeleport;

    // Start is called before the first frame update
    void Start()
    {
        fullFilePath = Path.Combine(folderPath, playerVector3Cords);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Load();
        }
    }
     
    public void Save()
    {
        dataToBeSavedRef.SetPlayerPosition(playerToTeleport.transform.position);

        string jsonData = JsonUtility.ToJson(dataToBeSavedRef);

        File.WriteAllText(fullFilePath, jsonData);

        Debug.Log("Saved to json file");
    }

    public void Load()
    {
        if (File.Exists(fullFilePath))
        {
            string jsonData = File.ReadAllText(fullFilePath);

            dataToBeSavedRef = JsonUtility.FromJson<DataToBeSaved>(jsonData);

            if (dataToBeSavedRef != null)
            {
                Debug.Log("New save loaded");
                playerToTeleport.transform.position = dataToBeSavedRef.ReturnPlayerPosition();
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
