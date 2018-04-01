using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {

	public void OnOkButton() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
