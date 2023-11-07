using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject BarrierP1;
    private GameObject BarrierP2;
    private GameObject BarrierP3;
    private GameObject BarrierP4;
    private List<GameObject> barrierList;

    private GameObject spawnP1;
    private GameObject spawnP2;
    private GameObject spawnP3;
    private GameObject spawnP4;
    private List<GameObject> spawnList;

    private PlayerConfigManager pcm; //playerconfigmanager
    private int playerNumber;
    public List<Player> playerList;
    private ScoreManager scoreMan;

    [SerializeField]
    private GameObject ballPrefab;
    public int maxScore;

    private float ballSpawnDelay = 1f;

    public static int roundWinnerId;

    

    private void Awake()
    {
        // get 4 spawn points
        spawnP1 = GameObject.Find("Player1Spawn");
        spawnP2 = GameObject.Find("Player2Spawn");
        spawnP3 = GameObject.Find("Player3Spawn");
        spawnP4 = GameObject.Find("Player4Spawn");

        // get 4 barriers
        BarrierP1 = GameObject.Find("BarrierP1");
        BarrierP2 = GameObject.Find("BarrierP2");
        BarrierP3 = GameObject.Find("BarrierP3");
        BarrierP4 = GameObject.Find("BarrierP4");
                                
        // get playerConfigMan
        pcm = GameObject.Find("PlayerConfigManager").GetComponent<PlayerConfigManager>();
        if (pcm != null)
        {
            Debug.Log("Players Connected: " + pcm.connectedPlayers.ToString());
        }
        // get scoreManager
        scoreMan = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();


        int playerNum = pcm.connectedPlayers;
        //player object list;
        playerList = new List<Player>();
        //spawn points list;
        spawnList = new List<GameObject> { spawnP1, spawnP2, spawnP3, spawnP4 };
        //spawn barriers list;
        barrierList = new List<GameObject> { BarrierP1, BarrierP2, BarrierP3, BarrierP4 };

        for (int i = 0; i < 4; i++) // 4 sides
        {
            Player player = new Player(i);
            if (i < playerNum)
            {
                player = player.newPlayer(spawnList[i].transform.position, barrierList[i]);
                playerList.Add(player);

            }
            else
            {
                player = player.nonPlayer();
                playerList.Add(player);
            }
        }
    }

    public void Start()
    {
        //spawn barriers
        barrierList = new List<GameObject> {BarrierP1 , BarrierP2 , BarrierP3 , BarrierP4};
        
        for (int i = 0; i < 4; i++) // 4 sides
        {
            if (playerList[i].isSpawned)
            {
                barrierList[i].SetActive(false);
            }
            else
            {
                barrierList[i].SetActive(true);
            }
        }

        //spawn ball;
        SpawnBall();
    }

    public void AddPlayerPrefab(GameObject playerPrefab, int playerId)
    {
        Player player = GetPlayerFromId(playerId);
        player.playerPrefab = playerPrefab;
    }

    public void CheckPlayerCount()
    {
        int count = playerList.Count(player => player.isAlive == true);
        Debug.Log(count);
        if (count < 2) 
        {
            int winnerId = playerList.Find(player => player.isAlive == true).playerIndex;
            ResetGameRound();
            UpdateScore(winnerId);
            roundWinnerId = winnerId;
        }
    }

    public void KillPlayer(int playerId)
    {
        Player player = GetPlayerFromId(playerId);
        player.Kill();
        CheckPlayerCount();
    }   

    public void SpawnBall()
    {
        StartCoroutine(SpawnBallWithDelay());
    }

    public void BorderCollision(int playerId)
    {
        SpawnBall();
        KillPlayer(playerId);
    }

    public void UpdateScore(int playerId)
    {
        ScoreManager.playerScores[playerId]++;
        SceneManager.LoadScene("ScoreScene");
    }

    public void ResetGameRound()
    {
        foreach (var player in playerList)
        {
            if (player.isSpawned)
            {
                player.ResetRound();
            }
        }
    }

    // coroutine fuunction to spawn ball;
    public IEnumerator SpawnBallWithDelay()
    {
        yield return new WaitForSeconds(ballSpawnDelay); // Wait for delay

        Instantiate(ballPrefab, ballPrefab.transform.position, ballPrefab.transform.rotation);
    }

    // PLAYER CLASS ;
    public class Player
    {
        public GameObject playerPrefab { get; set; }
        public int playerIndex { get; set; }
        public bool isAlive { get; set; }
        public bool isSpawned { get; set; }
        public string powerUp { get; set; }
        public Vector3 spawnPoint { get; set; }
        public GameObject barrier { get; set; }

        public Player(int pi)
        {
            playerIndex = pi;
        }

        // METHODS PLAYER CLASS ;

        public Player newPlayer(Vector3 newSpawnPoint, GameObject newBarrier) 
        { 
            Player player = new Player(playerIndex); 
            player.isSpawned = true;
            player.isAlive = true;
            player.spawnPoint = newSpawnPoint;
            player.barrier = newBarrier;
            return player;
        }

        public Player nonPlayer()
        {
            Player player = new Player(playerIndex);
            player.isSpawned = false;
            player.isAlive = false;
            return player;
        }       

        public void Kill()
        {
            if (isSpawned) {
                isAlive = false;
                playerPrefab.SetActive(false);
                barrier.SetActive(true);
            }
        }
        public void ResetRound() 
        {
            if (isSpawned)
            {
                isAlive = true;
                playerPrefab.transform.position = spawnPoint;
                playerPrefab.SetActive(true);
                barrier.SetActive(false);
            }
        }
    }

    public Player GetPlayerFromId(int playerId)
    {
        Player playerFind = playerList.Find(player => player.playerIndex == playerId);
        return playerFind;
    }

    public Player GetPlayerFromPrefab(GameObject playerGO)
    {
        Player playerFind = playerList.Find(player => player.playerPrefab == playerGO);
        return playerFind;
    }
}
