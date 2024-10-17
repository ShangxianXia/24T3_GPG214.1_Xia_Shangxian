using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextureStreaming : MonoBehaviour
{
    public string fileName = "SkullEmoji.png";
    public string SpriteName = "SkullSprite.png";
    public Image skullSprite;

    public string folderName = "Pictures";
    public string folderPath = Application.streamingAssetsPath;

    public string combinedFilePathLocation;
    public string combinedSpriteFilePathLocation;

        

    // Start is called before the first frame update
    void Start()
    {
        combinedSpriteFilePathLocation = "";

        combinedFilePathLocation = Path.Combine(folderPath, folderName, fileName);

        combinedSpriteFilePathLocation = Path.Combine(folderPath, folderName, SpriteName);

        LoadSprite();

        //LoadTexture();
    }

    //private void LoadTexture()
    //{
    //    Debug.Log("Attemping to load skull emoji on 3D object");

    //    if (File.Exists(combinedFilePathLocation))
    //    {
    //        Debug.Log("Loading the skull emoji on 3D object");

    //        // reads all bytes of data 
    //        byte[] imageBytes = File.ReadAllBytes(combinedFilePathLocation);

    //        // create a temporary texture to hold our texture in 
    //        Texture2D texture = new Texture2D(2, 2);
    //        texture.LoadImage(imageBytes); // takes bytes in and converts into image

    //        GetComponent<Renderer>().material.mainTexture = texture;
    //    }
    //    else
    //    {
    //        Debug.Log("Skull emoji not found, currently reading " +  combinedFilePathLocation);
    //    }
    //}

    private void LoadSprite()
    {
        Debug.Log("Attemping to load skull Sprite on 2D surface");

        if (File.Exists(combinedSpriteFilePathLocation))
        {
            Debug.Log("Loading the skull Sprite on 2D surface");

            // reads all bytes of data 
            byte[] spriteBytes = File.ReadAllBytes(combinedSpriteFilePathLocation);

            // create a temporary texture to hold our texture in 
            Texture2D spritetexture = new Texture2D(2, 2);
            spritetexture.LoadImage(spriteBytes); // takes bytes in and converts into image

            Sprite skullSprite = Sprite.Create(spritetexture, new Rect(0, 0, spritetexture.width, spritetexture.height), new Vector2(0.5f, 0.5f), 72);

            GetComponent<SpriteRenderer>().sprite = skullSprite;
        }
        else
        {
            Debug.Log("Skull emoji not found, currently reading " + combinedSpriteFilePathLocation);
        }
    }
}
