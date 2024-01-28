using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBallHelper : MonoBehaviour
{
    // Start is called before the first frame update

    public void Apply(GameObject target, float powerUpDuration)
    {
        target.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(turnOn(target, powerUpDuration));
    }
    IEnumerator turnOn(GameObject target, float powerUpDuration)
    {
        yield return new WaitForSeconds(powerUpDuration);
        target.GetComponent<MeshRenderer>().enabled = true;
    }
}
