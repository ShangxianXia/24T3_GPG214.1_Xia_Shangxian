using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    private AsyncOperation asyncOperation;
    public string spriteFileName = "SmallerSkull.png";
    public string folderName = "Pictures";
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        folderName = "Pictures";
        spriteFileName = "SmallerSkull.png";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("async loaded texture");
            StartCoroutine(AsyncLoadBoxTexture(spriteFileName));
        }
    }

    public IEnumerator AsyncLoadBoxTexture(string spriteFileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, folderName, spriteFileName);

        UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(filePath);
        asyncOperation = textureRequest.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        if (textureRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Path lookin at: " + filePath);
            Debug.Log("Failed texture request: " + textureRequest.error);
            yield break;
        }

        Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 32);
        spriteRenderer.sprite = sprite;

        textureRequest.Dispose();
    }

}
