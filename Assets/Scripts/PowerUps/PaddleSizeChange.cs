using System;
using UnityEngine;

public class PaddleSizeChange : PowerupEffect
{
    public float maxScale;
    public float minScale;
    public float xChange;

    private PlayerAttributes playerAttributes;
    AudioManager audioManager;
    public override void Apply(GameObject target)
    {
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

        playerAttributes = target.GetComponent<PlayerAttributes>();

        var currentScale = target.transform.localScale.y;
        var currentProgression = playerAttributes.sizePowerUpSigmoid;
        currentProgression += xChange;
        playerAttributes.sizePowerUpSigmoid = currentProgression;

        //math to make sigmoid x=0 always equal to 1 (starting scale of player)
        var yDiff = 1 - minScale;
        var yRange = maxScale - minScale;
        float a = (float) -Math.Log((yRange / yDiff) - 1);

        // Calculate the new scale using a sigmoid function
        var sigmoid = 1 / (1 + Math.Exp(-(currentProgression + a)));
        var newSize = minScale + (maxScale - minScale) * sigmoid;

        // Update the scale of the paddle with the correct sign
        var newScale = target.transform.localScale;
        newScale.y = (float) newSize;
        target.transform.localScale = newScale;
    }
}