using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class BundleStreaming : MonoBehaviour
{

    public string folderPath = "AssetBundles";
    public string fileName = "alansassets";
    private string combinedPath;
    private AssetBundle alansBundle;

    private Vector2 whereToInstantiatePushableSkull = new Vector2(19, 4);

    // Start is called before the first frame update
    void Start()
    {
        LoadAssetBundle();
        LoadPushableSkull();
    }

    public void LoadPushableSkull()
    {
        if (alansBundle == null)
        {
            Debug.Log("Alans bundle not working or theres nothing there");
        }

        GameObject pushableSkullPrefab = alansBundle.LoadAsset<GameObject>("Pushableskull");

        if (pushableSkullPrefab != null )
        {
            Instantiate(pushableSkullPrefab, whereToInstantiatePushableSkull, Quaternion.identity);
            LoadCorrectTexture(pushableSkullPrefab);
        }
        else
        {
            Debug.Log("something wrong with pushable skull prefab");
        }
    }

    public void LoadCorrectTexture(GameObject textureChange)
    {
        if (alansBundle == null)
        {
            Debug.Log("Alans bundle not working or theres nothing there");
        }

        Debug.Log("Beginning to load new image for pushable skull");

        Sprite pictureSprite = alansBundle.LoadAsset<Sprite>("SkullEmoji.png");


        if (textureChange != null)
        {
            Debug.Log("Loading new texture for pushable skull");

            textureChange.GetComponent<SpriteRenderer>().sprite = pictureSprite;
        }
    }

    private void LoadAssetBundle()
    {
        combinedPath = Path.Combine(Application.streamingAssetsPath, folderPath, fileName);

        if (File.Exists(combinedPath))
        {
            alansBundle = AssetBundle.LoadFromFile(combinedPath);
            Debug.Log("Alans bundle loaded");
        }
        else
        {
            Debug.Log("asset bundle path not there, currently in " + combinedPath);
        }
    }
}
