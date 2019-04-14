using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// score = in-game score,  hiScore = highest
    /// </summary>
    public int score, hiScore;

    private DataController dataController;


    public TextMeshProUGUI curScoreText;
    public TextMeshProUGUI hiScoreTextmenu, hiscoreTextGameOver, curScoreTextGameOver;



    void Awake()
    {
        score = 0;
        LoadGame();

        hiScoreTextmenu.text = "Hi-score: " + hiScore.ToString();

    }
	
	
	void Update ()
    {
        curScoreText.text = "Score: " + score.ToString();
        
        hiscoreTextGameOver.text = "Hi-score: " + hiScore.ToString();
        curScoreTextGameOver.text = "Score: " + score.ToString();


        
	}

    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        save.highestScore = hiScore;

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

    //    Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
           

            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            hiScore = save.highestScore;

         //   Debug.Log("Game Loaded");

        }
        else
        {
        //    Debug.Log("No game saved!");
        }
    }



    public void SubmitNewPlayerScore(int newScore)
    {
        if (newScore > hiScore)
        {
            hiScore = newScore;
            SaveGame();
        }
    }

}
