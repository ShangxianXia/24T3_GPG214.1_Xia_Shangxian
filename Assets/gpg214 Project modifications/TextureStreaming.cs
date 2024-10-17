using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TextureStreaming : MonoBehaviour
{
    public string spriteFileName = "Skull.png";
    public Image skullSprite;

    public string folderName = "Pictures";
    public string folderPath = Application.streamingAssetsPath;

    public string combinedSpriteFilePathLocation;

        

    // Start is called before the first frame update
    void Start()
    {
        spriteFileName = "Skull.png";
        combinedSpriteFilePathLocation = "";
        // finds the path of the skull png
        combinedSpriteFilePathLocation = Path.Combine(folderPath, folderName, spriteFileName);

        LoadSprite();
    }

    private void LoadSprite()
    {
        Debug.Log("Attemping to load skull Sprite on 2D surface");

        // checks if the file exists
        if (File.Exists(combinedSpriteFilePathLocation))
        {
            Debug.Log("Loading the skull Sprite on 2D surface");

            // reads all bytes of data 
            byte[] spriteBytes = File.ReadAllBytes(combinedSpriteFilePathLocation);

            // create a temporary texture to hold our texture in 
            Texture2D spritetexture = new Texture2D(2, 2);
            spritetexture.LoadImage(spriteBytes); // takes bytes in and converts into image

            // creates the sprite for the png to be in
            Sprite skullSprite = Sprite.Create(spritetexture, new Rect(0, 0, spritetexture.width, spritetexture.height), new Vector2(0.5f, 0.5f), 256);

            // makes the sprite component of the sprite renderer to be the new sprite
            GetComponent<SpriteRenderer>().sprite = skullSprite;
        }
        else
        {
            Debug.Log("Skull emoji not found, currently reading " + combinedSpriteFilePathLocation);
        }
    }
}
