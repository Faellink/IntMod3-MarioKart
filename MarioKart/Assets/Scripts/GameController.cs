using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //PLAYER
    //Numero de checkpoints, numero de voltas do jogador, numero do limite de voltas
    public int numCheckPoint;
    public int numLapPlayer;
    public int lapsLimit;

    //Inicio da volta, ganhou a corrida
    public bool playerLapStarted;
    public bool playerWins;

    //Tempo do jogador, ultima melhor volta, melhor volta 
    public float playerTime;
    public float lastTimePlayer;
    public float bestTimePlayer;

    //ENEMY
    public int numCheckPointEnemy;
    public int numLapEnemy;

    public bool enemyLapStarted;
    public bool enemyWins;

    public float enemyTime;
    public float lastTimeEnemy;
    public float bestTimeEnemy;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        //player
        numLapPlayer = 0;
        playerTime = 0.0f;
        playerWins = false;

        //enemy
        numLapEnemy = 0;
        enemyTime = 0.0f;
        enemyWins = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLapsCounter();
        EnemyLapsCounter();
    }

    public void PlayerLapsCounter()
    {
        if (playerLapStarted)
        {
            playerTime += Time.deltaTime;
        }
    }

    public void EnemyLapsCounter()
    {
        if (enemyLapStarted)
        {
            enemyTime += Time.deltaTime;
        }
    }

    public void UpdatePlayerLap()
    {
        numLapPlayer++;
        playerLapStarted = false;
        lastTimePlayer = playerTime;

        if (lastTimePlayer < bestTimePlayer)
        {
            bestTimePlayer = lastTimePlayer;
        }

        playerTime = 0;
        playerLapStarted = true;
    }

    public void UpdateEnemyLap()
    {
        numLapEnemy++;
        enemyLapStarted = false;
        lastTimeEnemy = enemyTime;

        if (lastTimeEnemy < bestTimeEnemy)
        {
            bestTimeEnemy = lastTimeEnemy;
        }

        enemyTime = 0;
        enemyLapStarted = false;
    }
}
