using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] Animator transition;
    public float durationTransition;
    // Start is called before the first frame update

    public void Awake()
    {
        var elements = GameObject.FindGameObjectsWithTag("TransitionManager");
        if (elements.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }
    
    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(durationTransition);
        SceneManager.LoadSceneAsync(sceneName);
        transition.SetTrigger("End");
    }
}
