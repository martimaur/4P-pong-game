using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GhostBall : PowerupEffect
{
    // Start is called before the first frame update
    public float powerUpDuration = 3f;
    private int powerUpCount;
    private Animator animator;
    AudioManager audioManager;


    public override void Apply(GameObject target)
    {
        // logic
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.ghostBall);

        animator = target.GetComponent<Animator>();
        animator.SetBool("isVisible", false);
        CoroutineManager.Instance.StartCoroutine(WaitAndRemovePowerup(target, powerUpDuration));
    }

    IEnumerator WaitAndRemovePowerup(GameObject target, float duration)
    {
        Debug.Log("started timer for");
        yield return new WaitForSeconds(duration);
        if (animator != null) {
            animator.SetBool("isVisible", true);
        }
        Debug.Log("finished timer");
    }
}   
