using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/PaddleSizeChange")]
public class PaddleSizeChange : PowerupEffect
{
    // Start is called before the first frame update
    public float percentageChange;
    public override void Apply(GameObject target)
    {
        var scale = target.transform.localScale;
        scale.y*=percentageChange; //scales paddle
        target.transform.localScale = scale;
    }
}
