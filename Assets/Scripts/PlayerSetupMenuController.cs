    using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSetupMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    private int PlayerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;

    
    private PlayerConfigManager playerConfigManager;

    private float ignoreInputTime = 0f;
    private bool inputEnabled;

    private void Awake()
    {
        playerConfigManager = GameObject.Find("PlayerConfigManager").GetComponent<PlayerConfigManager>();
    }
    public void SetPlayerIndex(int playerInd)
    {
        PlayerIndex = playerInd;
        titleText.SetText("Player " + (playerInd + 1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }
    public void SetPlayerMaterial(Material playerMaterial )
    {
        if(!inputEnabled) { return;  }
        PlayerConfigManager.instance.SetPlayerMaterial(PlayerIndex, playerMaterial);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void SetUiMaterial(Material uiMaterial)
    {
        if (!inputEnabled) { return; }
        PlayerConfigManager.instance.SetUiMaterial(PlayerIndex, uiMaterial);
    }

    public void ReadyPlayer()
    {
        if (!inputEnabled) { return; }
        PlayerConfigManager.instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);
        Debug.Log("Player " + (PlayerIndex+1).ToString() + " is ready!");
    }
}
