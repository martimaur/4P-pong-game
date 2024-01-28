using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static CoroutineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("CoroutineManager");
                _instance = obj.AddComponent<CoroutineManager>();
            }
            return _instance;
        }
    }

    public void StartPowerupEffectCoroutine(PowerupEffect powerupEffect, float duration)
    {
        StartCoroutine(PowerupEffectCoroutine(powerupEffect, duration));
    }

    private IEnumerator PowerupEffectCoroutine(PowerupEffect powerupEffect, float duration)
    {

        yield return new WaitForSeconds(duration);
    }
}
