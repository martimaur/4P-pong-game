using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DontDestroyOnLoadScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] string tagElement;
    [SerializeField] bool immortalScript; // does it not die when game restarts

    private void Awake()
    {
        if (this == null) { return; }

        if (!immortalScript)
        {
            GameOverManager.gameRestart += DestroyLoadObj; //add function to gameDeath
        }

        var elements = GameObject.FindGameObjectsWithTag(tagElement);
        if (elements.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
    }
    void Start()
    {
        if (this == null) { return; }
        DontDestroyOnLoad(gameObject);
    }

    public void DestroyLoadObj()
    {
        if (this == null) { return; }
        Destroy(gameObject);
        Destroy(this);
    }
}
