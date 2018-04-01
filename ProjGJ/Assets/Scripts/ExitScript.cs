using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour {

    public GameObject errorMesagePrefab;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            Debug.Log("LEVEL DONE!");
            int nextLvl = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
            if (nextLvl >= UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings) {
                GameObject temp = Instantiate(errorMesagePrefab, LevelManager.instance.player.transform.position, Quaternion.identity);
                temp.GetComponent<ErrorMessageController>().SetText("Ого, кажется, это победа. Чудеса.");
            }
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }
}
