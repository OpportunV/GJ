using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenu : MonoBehaviour {

	public void OnPlayButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OnQuitButton() {
        Debug.Log("Quitting!");
        Application.Quit();
    }
}
