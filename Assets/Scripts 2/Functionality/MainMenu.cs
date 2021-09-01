using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int SceneToLoad;
    [SerializeField] private GlobalManager globe;
    [SerializeField] private Inventory inventory;
    private void Start() {
        globe.SceneIndex = SceneToLoad;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveGame(){
        string jsonGlobal = JsonUtility.ToJson(globe);
        string jsonIventory = JsonUtility.ToJson(inventory);
        Debug.Log(jsonGlobal);
        Debug.Log(jsonIventory);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
