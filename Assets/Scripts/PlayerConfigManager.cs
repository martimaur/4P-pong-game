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
    public TransitionManager transitionMan;
    public PlayerInputManager playerManager;
    public static PlayerConfigManager instance {  get; private set; }

    private void Awake()
    {
        playerManager = GetComponent<PlayerInputManager>();
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

    public void Start()
    {
        // get transitionManager
        transitionMan = GameObject.Find("TransitionManager").GetComponent<TransitionManager>();
    }
    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }

    public void SetPlayerMaterial(int playerInd, Material player_mat)
    {
        playerConfigs[playerInd].playerMaterial = player_mat;
    }

    public void SetUiMaterial(int playerInd, Material ui_mat)
    {
        playerConfigs[playerInd].uiMaterial = ui_mat;
    }

    public void ReadyPlayer(int playerInd)
    {
        playerConfigs[playerInd].isReady = true;
        if (playerConfigs.Count > 1 &&  playerConfigs.Count <= maxPlayers && playerConfigs.All(p => p.isReady == true))
        {
            playerManager.DisableJoining();
            transitionMan.ChangeScene("MainScene");
            connectedPlayers = playerConfigs.Count;
        } 
    }

    public void HandlePlayerJoin(PlayerInput player)
    {
        Debug.Log("Player " + (player.playerIndex+1).ToString() + " joined!");
        controlsIcons.SetActive(false); //remove controls icons instructions
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

    public Material GetUiMaterial(int playerId)
    {
        foreach (var player in playerConfigs)
        {
            if (player.playerIndex == playerId)
            {
                return player.uiMaterial;
            }
        }
        Debug.Log("GET_UI_MATERIAL ERROR");
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
    public Material uiMaterial { get; set; }
}
