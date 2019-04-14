using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZObjectPools;


public class GameController : MonoBehaviour
{

    public float enemySpeed;

    public float repeatRate;

    public GameObject enemy;
    public GameObject player;

    public GameObject particleSpawnPlayer;
    public Transform playerEndPos;

    public Transform[] spawnPoints;

    public GameObject[] enemies;

    EZObjectPool objectPool;

    public int hitCount;

    public int targetHitCount;


    private float nextEnemyTime = 0.0f;

    UIController uiC;

    public bool gameOver;

    bool isRunning = false;

    
    
 
	void Start()
    {

        //InvokeRepeating("RandomizeRepeatRate", 10f, repeatRate);
        //  InvokeRepeating("SpeedIncrease", 20f, 20f);
        //    StartCoroutine(SpawnStuff());
        //     StartCoroutine(RandomTime());
     
        uiC = FindObjectOfType<UIController>();
	}
	

	void Update()
    {
    
    }


    public void CancelCoroutines()
    {
        StopCoroutine("SpawnStuff");
        StopCoroutine("RandomTime");
        StopCoroutine("SpeedBoost");
    }

    private void EnemySpawn()
    {
      
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

       Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

    }

    public void SpeedIncrease()
    {      
        enemySpeed += 0.05f;  
    }

    public void ClearEnemies() // when player dies, remove all enemies from screen
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject go in enemies)
        {
            go.GetComponent<EnemyController>().InstantKill();
            enemies.Equals(0);
                   
        }
    }

    private IEnumerator SpawnStuff()
    {

            while(!gameOver)
          {
    
            EnemySpawn();
            yield return new WaitForSeconds(repeatRate);
          //  StopCoroutine(SpawnStuff());
            
          }
    }

    private IEnumerator RandomTime()
    {
         while (!gameOver)
            {
     
            RandomizeRepeatRate();
            yield return new WaitForSeconds(repeatRate);
          //  StopCoroutine(RandomTime());
        }
    }

    private IEnumerator SpeedBoost()
    {
          while (!gameOver)
          {       
            SpeedIncrease();
            yield return new WaitForSeconds(20f);
          //  StopCoroutine(SpeedBoost());
          }
    }

  
    private void RandomizeRepeatRate()
    {
        // repeatRate = Random.Range(0.4f, 2f);
        repeatRate = Random.Range(1f, 1.5f);
    }

    public void Restart()
    {



      

        if (!player.activeSelf)
        {
            player.gameObject.SetActive(true);
        }

      

        if (gameOver)
        {
            Instantiate(particleSpawnPlayer, playerEndPos.position, Quaternion.identity);
            player.GetComponent<PlayerController>().ready = true;
            player.GetComponent<Animator>().SetBool("endGame", false);
            player.transform.position = new Vector3(0, 7.038f, 0);
            player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
          
        }


        gameOver = false;
        enemySpeed = 0.55f;
       
        StartCoroutine("SpawnStuff");
        StartCoroutine("RandomTime");
        StartCoroutine("SpeedBoost");
        uiC.CloseMenu(); // reset UI objects
        FindObjectOfType<ScoreManager>().score = 0;
        FindObjectOfType<BarScript>().DrainBar();



    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
    public IEnumerator WaitPlayerSpawn()
    {
        yield return new WaitForSeconds(1f);
        uiC.gameOverMenu.SetActive(true);
        Instantiate(particleSpawnPlayer, playerEndPos.position, Quaternion.identity);
        player.gameObject.SetActive(true);
        player.GetComponent<PlayerController>().ready = false;
        player.GetComponent<Animator>().SetBool("endGame", true);
        player.transform.position = playerEndPos.position;
        player.transform.rotation = Quaternion.Euler(0f, 126.091f, 0f);
    }


}
