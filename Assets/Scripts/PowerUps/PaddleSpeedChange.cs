using System;
using UnityEngine;

public class PaddleSpeedChange : PowerupEffect
{
    public float maxSpeed;
    public float minSpeed;
    public float xChange = 1.0f; // You can adjust this factor based on your needs

    private PlayerAttributes playerAttributes;
    AudioManager audioManager;

    public override void Apply(GameObject target)
    {
        var mover = target.GetComponent<Mover>();

        //sfx audio logic
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        if (xChange > 0) //buff 
        {
            audioManager.PlaySFX(audioManager.buff);
        }
        else //debuff
        {
            audioManager.PlaySFX(audioManager.debuff);
        }

        if (mover != null)
        {
            playerAttributes = target.GetComponent<PlayerAttributes>();
            var inicialSpeed = mover.inicialSpeed;

            var currentProgression = playerAttributes.speedPowerUpSigmoid;
            currentProgression += xChange;
            playerAttributes.speedPowerUpSigmoid = currentProgression;

            //math to make sigmoid x=0 always equal to 1 (starting speed of player)
            var yDiff = 1 - minSpeed;
            var yRange = maxSpeed - minSpeed;
            float a = (float)-Math.Log((yRange / yDiff) - 1);

            // Calculate the new scale using a sigmoid function
            var sigmoid = 1 / (1 + Math.Exp(-(currentProgression + a)));

            float newSpeed = minSpeed + (maxSpeed - minSpeed) * (float) sigmoid;
                
            // Update the scale of the paddle with the correct sign
            mover.MoveSpeed = newSpeed * inicialSpeed;
        }
        else
        {
            Debug.LogError("Mover component not found on the target GameObject.");
        }
    }
}