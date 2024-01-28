using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public abstract class PowerupEffect : MonoBehaviour
{    
    public abstract void Apply(GameObject target);
}
