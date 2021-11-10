using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public enum CheckPoints
    {
        One,
        Two,
        Three,
        Four,
    };

    public GameController gameController;
    public CheckPoints cp;

    private void Start()
    {
        gameController.playerLapStarted = false;
        gameController.numCheckPoint = 0;
        gameController.numLapPlayer = 0;
        gameController.bestTimeEnemy = int.MaxValue;
        gameController.bestTimePlayer = int.MaxValue;
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (cp == CheckPoints.One)
            {
                if (gameController.numCheckPoint == 0 && gameController.numLapPlayer == 0) //
                {
                    gameController.playerLapStarted = true;
                    gameController.numCheckPoint = 1;
                }
                else if (gameController.numCheckPoint == 4)
                {
                    gameController.UpdatePlayerLap();
                    gameController.numCheckPoint = 1;
                }
                else if (gameController.numCheckPoint == 1 && gameController.numLapPlayer == 3)
                {
                    gameController.playerWins = true;
                }
            }
            else if (cp == CheckPoints.Two && gameController.numCheckPoint == 1)
            {
                gameController.numCheckPoint = 2;
            }
            else if (cp == CheckPoints.Three && gameController.numCheckPoint == 2)
            {
                gameController.numCheckPoint = 3;
            }
            else if (cp == CheckPoints.Four && gameController.numCheckPoint == 3)
            {
                gameController.numCheckPoint = 4;
            }
        }
        else if (coll.CompareTag("enemy"))
        {
            if (cp == CheckPoints.One)
            {
                if (gameController.numCheckPointEnemy == 0 && gameController.numLapEnemy == 0)
                {
                    gameController.enemyLapStarted = true;
                    gameController.numCheckPointEnemy = 1;
                }
                else if (gameController.numCheckPointEnemy == 4)
                {
                    gameController.UpdateEnemyLap();
                    gameController.numCheckPointEnemy = 1;
                }
                else if (gameController.numCheckPointEnemy == 1 && gameController.numLapEnemy == 3)
                {
                    gameController.enemyWins = true;
                }
            }
            else if (cp == CheckPoints.Two && gameController.numCheckPointEnemy == 1)
            {
                gameController.numCheckPointEnemy = 2;
            }
            else if (cp == CheckPoints.Three && gameController.numCheckPointEnemy == 2)
            {
                gameController.numCheckPointEnemy = 3;
            }
            else if (cp == CheckPoints.Four && gameController.numCheckPointEnemy == 3)
            {
                gameController.numCheckPointEnemy = 4;
            }
        }
    }
}