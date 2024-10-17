using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataToBeSaved
{
    public float[] playerPosition = new float[] { 0, 0, 0 };
    public int timesThePlayerSaved;
    public int timesThePlayerLoaded;
    public string nameToBeSaved;

    public Vector3 ReturnPlayerPosition()
    {
        if (playerPosition.Length < 3)
        {
            Debug.Log("Not enough vectors or didnt save for player");
            return Vector3.zero;
        }

        return new Vector3(playerPosition[0], playerPosition[1], playerPosition[2]);
    }

    public void SetPlayerPosition(Vector3 playerReposition)
    {
        playerPosition[0] = playerReposition.x;
        playerPosition[1] = playerReposition.y;
        playerPosition[2] = playerReposition.z;
    }
}
