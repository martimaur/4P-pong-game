using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : PowerupEffect
{
    public GameObject spikeBallGameObject;
    AudioManager audioManager;
    // Start is called before the first frame update
    public override void Apply(GameObject target)
    {
        //sfx audio logic
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.spikeBall);

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
