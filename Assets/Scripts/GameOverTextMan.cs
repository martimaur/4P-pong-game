using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverTextMan : MonoBehaviour
{
    private PlayerConfigManager pcm;
    private Material component;
    private TextMeshProUGUI playerWonText;
    private TextMeshPro playerWonTextAlt;


    private void Start()
    {
        playerWonText = GameObject.Find("PlayerWon").GetComponent<TextMeshProUGUI>();
        int winnerId = GameManager.roundWinnerId;
        pcm = GameObject.Find("PlayerConfigManager").GetComponent<PlayerConfigManager>();

        Material materialWinner = pcm.GetPlayerMaterial(winnerId);

        playerWonText.text = "PLAYER " + (winnerId + 1).ToString() + " WON! ";
        playerWonText.color = materialWinner.color;

    }
}
