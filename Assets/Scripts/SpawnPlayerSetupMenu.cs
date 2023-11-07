using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{

    public GameObject playerSetupMenuPrefab;
    private GameObject rootMenu;
    public PlayerInput input;

    private void Awake()
    {
        var rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null )
        {
            Debug.Log("\nInstantiated.\n");
            var menu = Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();
            menu.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(input.playerIndex);
        }
        else
        {
            Debug.Log("\nSpawnPlayerSetupMenu error, MainLayout not found.\n");
        }
    }

}
