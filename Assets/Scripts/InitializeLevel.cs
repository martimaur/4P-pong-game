using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;
    [SerializeField]
    private GameObject playerPrefabHorizontal;
    [SerializeField]
    private GameObject playerPrefabVertical;
    private GameManager gm;

    // Start is called before the first frame update

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();    

        if (PlayerConfigManager.instance==null) { return; }
        var playerConfigs = PlayerConfigManager.instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++) 
        {
            if (i<2)
            {
                var player = Instantiate(playerPrefabVertical, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<PlayerInputHandler>().InitPlayer(playerConfigs[i]);
                gm.AddPlayerPrefab(player, i);
            }
            else
            {
                var player = Instantiate(playerPrefabHorizontal, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
                player.GetComponent<PlayerInputHandler>().InitPlayer(playerConfigs[i]);
                gm.AddPlayerPrefab(player, i);
            }
            
        }
    }

}
