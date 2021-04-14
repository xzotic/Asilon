using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField] private Image _ProgressBar;
    public void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation GameLevel = SceneManager.LoadSceneAsync(2);
        while (GameLevel.progress < 1)
        {
            _ProgressBar.fillAmount =  GameLevel.progress;
            yield return new WaitForEndOfFrame();           
        }
    }
}
