using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/PaddleSizeChange")]
public class PaddleSizeChange : PowerupEffect
{
    public float maxScale;
    public float minScale;
    public float xChange;

    private PlayerAttributes playerAttributes;

    public override void Apply(GameObject target)
    {
        playerAttributes = target.GetComponent<PlayerAttributes>();

        var currentScale = target.transform.localScale.y;
        var currentProgression = playerAttributes.sizePowerUpSigmoid;
        Debug.Log("current sigmoid progression: "+ currentProgression + "+ "+xChange);
        currentProgression += xChange;
        playerAttributes.sizePowerUpSigmoid = currentProgression;

        Debug.Log(maxScale+", "+minScale+", "+xChange);
        //math to make sigmoid x=0 always equal to 1 (starting scale of player)
        var yDiff = 1 - minScale;
        var yRange = maxScale - minScale;
        Debug.Log(yDiff +"/"+ yRange);
        float a = (float) -Math.Log((yRange / yDiff) - 1);
        Debug.Log("a: "+a);

        // Calculate the new scale using a sigmoid function
        var sigmoid = 1 / (1 + Math.Exp(-(currentProgression + a)));
        Debug.Log(sigmoid);
        var newSize = minScale + (maxScale - minScale) * sigmoid;
        Debug.Log(newSize.ToString());
        Debug.Log(sigmoid);

        // Update the scale of the paddle with the correct sign
        var newScale = target.transform.localScale;
        newScale.y = (float) newSize;
        Debug.Log(newScale.ToString());
        target.transform.localScale = newScale;
    }
}