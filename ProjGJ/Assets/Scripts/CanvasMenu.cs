using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMenu : MonoBehaviour {

	public void OnPlayButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void OnQuitButton() {
        Application.Quit();
    }
}
