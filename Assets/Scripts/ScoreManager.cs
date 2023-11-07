using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScoreManager : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField]
    private GameObject scoreSprite;
    [SerializeField]
    private GameObject activeScoreSprite;
    [SerializeField]
    private GameObject horizontalRowPrefab;
    [SerializeField]
    private GameObject verticalRowPrefab;

    private PlayerConfigManager pcm;
    private GameManager gm;

    private GameObject MainLayout;
    [SerializeField]
    private int maxScore = 5;

    private int playerCount;
    [SerializeField]
    private float changeSceneTime = 1.5f;

    private List<GameObject> spritesHorizontalRow;
    private List<List<GameObject>> horizontalRows;

    public static List<int> playerScores;
    private GameObject duplicate;
    private bool firstRun = true;

    private void Awake()
    {
        //subscribe to event
        SceneManager.activeSceneChanged += ChangedActiveScene;
        pcm = GameObject.Find("PlayerConfigManager").GetComponent<PlayerConfigManager>();
        playerCount = pcm.connectedPlayers;

        if (playerScores == null)
        {
            playerScores = new List<int> { 0, 0, 0, 0 }; // initializes the list at only 0's
        }
    }

    private void ChangedActiveScene(UnityEngine.SceneManagement.Scene prev, UnityEngine.SceneManagement.Scene active)
    {
        //checks on scene
        if (this == null) { return; }
        if (active.name  != "ScoreScene") { return; }

        //function calls;
        if (firstRun) { firstRun = false; FirstRunManager(); }
        UpdateScore(GameManager.roundWinnerId);
        if (CheckWinner()) { return; }
        RechangeScene("MainScene");
    }

    private void FirstRunManager()
    {
        MainLayout = GameObject.Find("MainLayout"); 
        MainLayout.SetActive(false);
        GameObject verticalInstance = createInst(verticalRowPrefab, MainLayout); //create a new instance of the vertical row
        for (int y = 0; y < playerCount; y++)
        {
            GameObject horizontalInstance = createInst(horizontalRowPrefab, verticalInstance); //create a new instance of a horizontal row
            for (int j = 0; j < maxScore; j++)
            {
                var inst = createInst(scoreSprite, horizontalInstance); //add new sprite to the instance
            }
        }
    }

    private void UpdateScore(int playerId) //visual update
    {
        MainLayout.gameObject.SetActive(true);
        var newSprite = activeScoreSprite;
        newSprite.GetComponent<UnityEngine.UI.Image>().material = pcm.GetPlayerMaterial(playerId);

        var parent = MainLayout.transform.GetChild(0).transform.GetChild(playerId+1).transform.GetChild(playerScores[playerId]-1).gameObject;
        createInst(newSprite, parent);
    }

    private bool CheckWinner()
    {
        int cnt = 0;
        int winnerId;
        bool foundWinner = false;
        foreach(var elem in playerScores)
        {
            if (elem >= maxScore)
            {
                winnerId = cnt;
                foundWinner = true;
            }
            cnt++;
        }
        if (!foundWinner) {
            return false;
        }
        clearMemory();
        RechangeScene("GameOverScene");
        return true;
    }

    private void clearMemory()
    {
        playerScores = new List<int> { 0, 0, 0, 0 };

    }

    private GameObject createInst(GameObject prefab, GameObject parent)
    {
        var instance = Instantiate(prefab, prefab.transform.position, prefab.transform.rotation);
        instance.transform.SetParent(parent.transform, false);
        instance.transform.position= parent.transform.position;
        return instance;
    }

    public void RechangeScene(string sceneName)
    {
        if (this == null) { return; }
        StartCoroutine(DelaySceneChange(sceneName));
    }

    IEnumerator DelaySceneChange(string sceneName)
    {
        yield return new WaitForSeconds(changeSceneTime); // Wait for delay
        MainLayout.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }

    private void PrintListElements<T>(List<T> listToPrint)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Scores: ");
        foreach (T item in listToPrint)
        {
            sb.Append(item.ToString());
            sb.Append(" - "); // Add a space as a separator
        }

        // Remove the trailing space and print the result
        sb.Remove(sb.Length - 3, 3);
        Debug.Log(sb.ToString().Trim());
    }
}


