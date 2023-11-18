using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool applyPaddle;
    public bool applyBall;
    public PowerupEffect powerupEffect;
    private PowerUpManager powerupManager;
    public GameObject animationEffect;

    private void Awake()
    {
        powerupManager = GameObject.Find("PowerUpManager").GetComponent<PowerUpManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //check if ball hit powerup;
        if (other.transform.tag == "Ball")
        {
            var obj = Instantiate(animationEffect, this.transform.position, animationEffect.transform.rotation); //spawn fx
            Debug.Log("Instantiated new effect: "+obj);

            if (applyBall) 
            {
                powerupEffect.Apply(other.gameObject);
            }
            if (applyPaddle)
            {
                var lastPlayer = other.GetComponent<BallController>().lastPlayerTouch;
                if (lastPlayer != null) powerupEffect.Apply(lastPlayer.gameObject);
            }
            Destroy(gameObject);
            powerupManager.RemovePowerUp(gameObject);
            //remove powerup icon
        }
    }
}
