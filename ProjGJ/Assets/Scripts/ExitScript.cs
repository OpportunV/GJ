using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour {

    public GameObject errorMesagePrefab;

    private bool victory = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (victory) {
            return;
        }
        if (collision.CompareTag("Player")) {
            Debug.Log("LEVEL DONE!");
            int nextLvl = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
            if (nextLvl >= UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings) {
                GameObject temp = Instantiate(errorMesagePrefab, LevelManager.instance.player.transform.position, Quaternion.identity);
                temp.GetComponent<ErrorMessageController>().SetText("Wow. It seems you're won. What's a miracle!");
                Invoke("LoadMenu", 2f);
                victory = true;
                return;
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextLvl % UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings);
        }
    }

    void LoadMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
