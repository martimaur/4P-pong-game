using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public delegate void GameRestart();
    public static GameRestart gameRestart;
    public static GameRestart pcmRestart; 

    // Start is called before the first frame update
    public void RestartGame()
    {
        gameRestart();
        SceneManager.LoadScene("MainScene");
    }

    public void ResetGame()
    {
        gameRestart();
        pcmRestart();
        SceneManager.LoadScene("PlayerSetup");
    }

    private void clearMemory()
    {

    }

}