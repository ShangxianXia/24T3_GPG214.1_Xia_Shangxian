using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    [SerializeField] private JSONSave jsonSaveRef;

    public Texture skullPicture;
    public AudioClip audioClip;
    public AssetBundle alansBundle;

    public string jsonFileName;
    public string jsonData;

    public string streamingAssetFolderPath = Application.streamingAssetsPath;

    IEnumerator Start()
    {
        jsonSaveRef = GameObject.Find("JsonSaving").GetComponent<JSONSave>();

        jsonFileName = "JSONFile.json";
        yield return StartCoroutine(AsyncLoadingJsonFile());


    }

    private void Update()
    {
        
    }

    IEnumerator AsyncLoadingJsonFile()
    {
        UnityWebRequest jsonDataRequest = UnityWebRequest.Get(Path.Combine(streamingAssetFolderPath, jsonFileName));

        AsyncOperation downloadingJsonData = jsonDataRequest.SendWebRequest();

        jsonData = jsonDataRequest.downloadHandler.text;

        yield return null;
    }

}
