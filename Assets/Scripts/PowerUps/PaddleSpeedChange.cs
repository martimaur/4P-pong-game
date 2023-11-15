using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/PaddleSpeedChange")]
public class PaddleSpeedChange : PowerupEffect
{
    public float xChange = 1.0f; // You can adjust this factor based on your needs
    public float minSpeed;
    public float maxSpeed;

    private PlayerAttributes playerAttributes;

    public override void Apply(GameObject target)
    {
        var mover = target.GetComponent<Mover>();

        if (mover != null)
        {
            playerAttributes = target.GetComponent<PlayerAttributes>();
            var inicialSpeed = mover.inicialSpeed;

            var currentProgression = playerAttributes.speedPowerUpSigmoid;
            Debug.Log("current sigmoid progression: " + currentProgression + "+ " + xChange);
            currentProgression += xChange;
            playerAttributes.speedPowerUpSigmoid = currentProgression;

            Debug.Log(maxSpeed + ", " + minSpeed + ", " + xChange);
            //math to make sigmoid x=0 always equal to 1 (starting speed of player)
            var yDiff = 1 - minSpeed;
            var yRange = maxSpeed - minSpeed;
            Debug.Log(yDiff + "/" + yRange);
            float a = (float)-Math.Log((yRange / yDiff) - 1);
            Debug.Log("a: " + a);

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