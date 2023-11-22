using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public delegate void GameRestart();
    public static GameRestart gameRestart;
    public static GameRestart pcmRestart;
    public TransitionManager transitionMan;

    // Start is called before the first frame update
    public void RestartGame()
    {
        transitionMan = GameObject.Find("TransitionManager").GetComponent<TransitionManager>();
        transitionMan.ChangeScene("MainScene");
        gameRestart();
    }

    public void ResetGame()
    {
        transitionMan = GameObject.Find("TransitionManager").GetComponent<TransitionManager>();
        transitionMan.ChangeScene("PlayerSetup");
        pcmRestart();
        gameRestart();
    }

    private void clearMemory()
    {

    }
}