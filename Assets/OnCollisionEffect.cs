using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fx;
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(fx, collision.transform.position, collision.transform.rotation);
    }
}
