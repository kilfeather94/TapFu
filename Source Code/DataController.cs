using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;


public class DataController : MonoBehaviour

{

    private ScoreManager scoreManage;

    private string gameDataFileName = "data.json";

    private int highestScore;


    void Start()
    {
        LoadGameData();

        LoadScores();
    }


    void Update()
    {

    }

  


    private void SaveScore()
    {
        PlayerPrefs.SetInt("highestScore", scoreManage.hiScore);
    }

    private void LoadGameData()
    {
        // Path.Combine combines strings into file path
        //Application.StreamingAssets points to Assets/StreamingAssets in Editor and StreamingAssets folder in build
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            // Retrieve the allRoundData property of loadedData
            highestScore = loadedData.hiScore;
        }

        else
        {
            Debug.LogError("Cannot load game data!");
        }
    }


    private void LoadScores()
    {
        scoreManage = new ScoreManager();

        // If PlayerPrefs contains a key called "highestScore", set the value of playerProgress.highestScore using the value associated with that key
        if (PlayerPrefs.HasKey("highestScore"))
        {
            scoreManage.hiScore = PlayerPrefs.GetInt("highestScore");
        }
    }

}
