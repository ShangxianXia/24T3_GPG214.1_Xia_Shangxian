using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AudioStreaming : MonoBehaviour
{
    public string fileName = "oof.mp3";

    public string folderName = "Audios";
    public string folderPath = Application.streamingAssetsPath;

    private string combinedFilePathLocation;

    private AudioSource audioSource;
    public AudioClip theAudioThatsBeingPlayed;

    // Start is called before the first frame update
    void Start()
    {
        combinedFilePathLocation = Path.Combine(folderPath, folderName, fileName);

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null )
        {
            Debug.Log("Component that plays audio is not there, e.g GetComponent<AudioSource>");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Thing that touched audio player is " + collision.name);
        if (collision.gameObject.name == "Ellen")
        {
            LoadSoundFile();
            playSoundFile();
        }
    }

    public void LoadSoundFile()
    {
        if (File.Exists(combinedFilePathLocation))
        {
            // store physical data in this array
            byte[] audioData = File.ReadAllBytes(combinedFilePathLocation);

            // convert byte array to float array, dividing by 2 due to its 2 bits for a byte
            float[] floatArray = new float[audioData.Length / 2];

            // looping the array 
            for (int i = 0; i < floatArray.Length; i++)
            {
                // converting audio data to 16 bit integer, moving offset by 2 each time
                short bitValue = System.BitConverter.ToInt16(audioData, i * 2);

                // normalising current value between -1, 1 with 32768 being max possible value
                floatArray[i] = bitValue / 32768.0f;
            }

            // call the create function
            theAudioThatsBeingPlayed = AudioClip.Create("Oof", floatArray.Length, 1, 44100, false);

            // sets the audio data
            theAudioThatsBeingPlayed.SetData(floatArray, 0);
        }
        else
        {
            Debug.Log("No sound file found in " + combinedFilePathLocation);
        }
    }

    private void playSoundFile()
    {
        if (audioSource == null || theAudioThatsBeingPlayed == null)
        {
            Debug.Log("Theres no audio to be played");
            return;
        }
        audioSource.PlayOneShot(theAudioThatsBeingPlayed);
    }
}
