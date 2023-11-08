using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    public int connectedPlayers;
    public GameObject controlsIcons;
    public int maxPlayers = 4;

    public static PlayerConfigManager instance {  get; private set; }

    private void Awake()
    {

        GameOverManager.pcmRestart += DestroyConfigManager;
        controlsIcons = GameObject.Find("ControlsIcons");
        if (instance != null)
        {
            Debug.Log("ERROR: Trying to create a second instance of singleton.");
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerMaterial(int playerInd, Material mat)
    {
        playerConfigs[playerInd].playerMaterial = mat;
    }
    public void ReadyPlayer(int playerInd)
    {
        playerConfigs[playerInd].isReady = true;
        if (playerConfigs.Count > 1 &&  playerConfigs.Count <= maxPlayers && playerConfigs.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("MainScene");
            connectedPlayers = playerConfigs.Count;
        } 
    }

    public void HandlePlayerJoin(PlayerInput player)
    {
        Debug.Log("Player " + (player.playerIndex+1).ToString() + " joined!");
        controlsIcons.SetActive(false); //remove controls icons instruction
        if (!playerConfigs.Any(p => p.playerIndex == player.playerIndex))
        { 
            player.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(player));
        }
    }

    public Material GetPlayerMaterial(int playerId)
    {
        foreach (var player in playerConfigs)
        {
            if (player.playerIndex == playerId)
            {
                return player.playerMaterial;
            }
        }
        Debug.Log("GETPLAYERMATERIAL ERROR");
        return null;
    }

    private void DestroyConfigManager()
    {
        if (this == null) { return; }
        Destroy(gameObject);
    }
}



public class PlayerConfiguration
{
    public PlayerConfiguration (PlayerInput player)
    {
        playerIndex = player.playerIndex;   
        input = player;
    }
    public PlayerInput input { get; set; }
    public int playerIndex { get; set; }
    public bool isReady { get; set; }
    public Material playerMaterial { get; set; }
}
