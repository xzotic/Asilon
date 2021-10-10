using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int SceneToLoad;
    [SerializeField] private GlobalManager globe;
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerMovement playerMovement;
    private string jsonGlobal;
    private void Start() {
        //string json = File.ReadAllText(Application.dataPath + "/Resources/globalsave.json");
        //GlobalManager loaded = JsonUtility.FromJson<GlobalManager>(jsonGlobal);
        globe.SceneIndex = SceneToLoad;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void InvUI() {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.UIInventory.gameObject.SetActive(true);
        playerMovement.UIInventory.SetInventory(playerMovement.inventory);
        playerMovement.UIInventory.ItemSlotTemplate.gameObject.SetActive(false);
        playerMovement.IsInventory = true;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
