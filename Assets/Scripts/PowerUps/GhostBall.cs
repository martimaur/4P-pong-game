using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName = "Powerups/GhostBall")]
public class GhostBall : PowerupEffect
{
    // Start is called before the first frame update
    public float powerUpDuration = 3f;
    private int powerUpCount;
    private MeshRenderer renderer;

    public override void Apply(GameObject target)
    {
        target.GetComponent<MeshRenderer>().enabled = false;
    }

}   
