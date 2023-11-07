using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/PaddleSizeChange")]
public class PaddleSizeChange : PowerupEffect
{
    // Start is called before the first frame update
    public float changeValue;
    public override void Apply(GameObject target)
    {
        var scale = target.transform.localScale;
        scale.y += changeValue; //scales paddle
        target.transform.localScale = scale;
    }
}
