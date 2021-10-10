using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransit : MonoBehaviour
{
    public string SceneToLoad;
    public Vector3 PlayerPosition;
    public GlobalManager PlayerStorage;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")&&!other.isTrigger){
            PlayerStorage.InitialValue = PlayerPosition;
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
