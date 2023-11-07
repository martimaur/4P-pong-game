using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/PaddleSpeedChange")]
public class PaddleSpeedChange : PowerupEffect
{
    // Start is called before the first frame update
    public float changeValue;
    public override void Apply(GameObject target)
    {
        target.GetComponent<Mover>().MoveSpeed += changeValue;
    }
}
