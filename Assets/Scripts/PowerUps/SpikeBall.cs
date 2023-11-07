using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpikeBall")]
public class SpikeBall : PowerupEffect
{
    // Start is called before the first frame update
    public override void Apply(GameObject target)
    {
        target.GetComponent<BallController>().powerUpEffect = "SpikeBall";
    }
}
