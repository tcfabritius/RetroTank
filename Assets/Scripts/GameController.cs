using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Spawner spawner;

    public float score;
    public float scorePerTank;
    public int lives;
    public int enemyStartingAmount;
    public int maxEnemiesAmount;

    private int currentLives;
    private int currentEnemyAmount;

    private GameObject player;
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < enemyStartingAmount; i++)
        {
            spawner.SpawnEnemy();
        }

        player = spawner.SpawnPlayer();

        score = 0f;
        currentLives = lives;
        currentEnemyAmount = enemyStartingAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLives > 0)
        {
            if (Input.GetButtonDown("Restart"))
            {
                if (player == null)
                {
                    player = spawner.SpawnPlayer();
                    currentLives--;
                }
            }
        }
        
    }

    public void EnemyDestroyed()
    {
        spawner.SpawnEnemy();
        score += scorePerTank;
        if(currentEnemyAmount < maxEnemiesAmount)
        {
            spawner.SpawnEnemy();
            currentEnemyAmount++;
        }
    }

    public void SetHealth(float current, float maxHealth)
    {
        if(current < 0)
        {
            current = 0f;
        }
    }
 
}
