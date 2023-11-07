using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/GhostBall")]
public class GhostBall : PowerupEffect
{
    // Start is called before the first frame update
    public override void Apply(GameObject target)
    {
        target.GetComponent<MeshRenderer>().enabled = false;
    }
}
