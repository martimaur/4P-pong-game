using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationLimit : MonoBehaviour
{
    public Animation myAnimation; // Reference to the Animation component

    private void Awake()
    {
        myAnimation.Play();
    }
}

