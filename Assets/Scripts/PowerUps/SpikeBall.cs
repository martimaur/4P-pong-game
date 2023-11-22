using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/SpikeBall")]
public class SpikeBall : PowerupEffect
{
    public GameObject spikeBallGameObject;
    // Start is called before the first frame update
    public override void Apply(GameObject target)
    {
        var ballController = target.GetComponent<BallController>();
        var speed = target.GetComponent<Rigidbody>().velocity; 
        var ballTransform = target.transform;
        Destroy(target);
        var spikeBall = Instantiate(spikeBallGameObject, ballTransform.position, spikeBallGameObject.transform.rotation);
        var spikeController = spikeBall.GetComponent<BallController>();
        spikeBall.GetComponent<Rigidbody>().velocity = speed;
        spikeBall.GetComponent<Rigidbody>().AddTorque(speed, ForceMode.Impulse);
        spikeController.powerUpEffect = "SpikeBall";
        spikeController.lastPlayerTouch = ballController.lastPlayerTouch;
    }
}
